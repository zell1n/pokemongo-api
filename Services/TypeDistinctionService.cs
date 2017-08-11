using System;
using System.Collections.Generic;
using System.Linq;
using pokemongo_api.DataAccess;
using pokemongo_api.Helpers;
using pokemongo_api.Models;

namespace pokemongo_api.Services
{
    public class TypeDistinctionService : ITypeDistinctionService
    {
        private readonly IS3DataAccess _s3DataAccess;
        private readonly IAwsConfiguration _awsConfiguration;

        public TypeDistinctionService(IS3DataAccess s3DataAccess, IAwsConfiguration awsConfiguration)
        {
            _s3DataAccess = s3DataAccess;
            _awsConfiguration = awsConfiguration;
        }

        public IEnumerable<TypeDistinction> GetTypeDistinctions()
        {
            var stream = _s3DataAccess.GetS3Object(_awsConfiguration.S3Config.BucketName, _awsConfiguration.S3Config.TypeDistinctionFile);

            return JsonHelper.DeserializeStream<TypeDistinction>(stream);
        }

        public TypeDistinction GetTypeDistinctionByType(string type)
        {
            var stream = _s3DataAccess.GetS3Object(_awsConfiguration.S3Config.BucketName, _awsConfiguration.S3Config.TypeDistinctionFile);

            return JsonHelper.DeserializeStream<TypeDistinction>(stream).ToList().FirstOrDefault(x => x.Type.ToLower() == type.ToLower());
        }
    }
}
