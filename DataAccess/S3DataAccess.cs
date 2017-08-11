using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Newtonsoft.Json;

namespace pokemongo_api.DataAccess
{
    public class S3DataAccess : IS3DataAccess
    {
        private readonly IAwsConfiguration _awsConfiguration;

        public S3DataAccess(IAwsConfiguration awsConfiguration)
        {
            _awsConfiguration = awsConfiguration;
        }

        public MemoryStream GetS3Object(string bucketName, string bucketItem)
        {
            var client = new AmazonS3Client(Amazon.RegionEndpoint.EUWest1);
            var response = client.GetObjectAsync(bucketName, bucketItem).Result;

            var json = response.ResponseStream.ToString();

            MemoryStream stream = new MemoryStream();
            response.ResponseStream.CopyTo(stream);
            return stream;
        }
    }
}
