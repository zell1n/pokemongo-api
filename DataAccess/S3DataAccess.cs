using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Newtonsoft.Json;

namespace pokemongo_api.DataAccess
{
    public class S3DataAccess
    {
        public async Task<Stream> GetS3Object()
        {
            var client = new AmazonS3Client();
            var response = await client.GetObjectAsync("pokemongo-api-data", "pokemons_api.json");
            
            return response.ResponseStream;
        }
    }
}
