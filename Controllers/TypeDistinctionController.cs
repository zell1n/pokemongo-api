using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using pokemongo_api.DataAccess;
using pokemongo_api.Helpers;
using pokemongo_api.Models;

namespace pokemongo_api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class TypeDistinctionController : Controller
    {
        private readonly IS3DataAccess _s3DataAccess;
        private readonly IAwsConfiguration _awsConfiguration;

        public TypeDistinctionController(IS3DataAccess s3DataAccess, IAwsConfiguration awsConfiguration)
        {
            _s3DataAccess = s3DataAccess;
            _awsConfiguration = awsConfiguration;
        }

        [HttpGet]
        public IActionResult GetTypeDistinctions()
        {
            var stream = _s3DataAccess.GetS3Object(_awsConfiguration.S3Config.BucketName, _awsConfiguration.S3Config.TypeDistinctionFile);

            return Ok(JsonHelper.DeserializeStream<TypeDistinction>(stream));
        }

        [HttpGet("type/{type}")]
        public IActionResult GetTypeDistinctionByType(string type)
        {
            var stream = _s3DataAccess.GetS3Object(_awsConfiguration.S3Config.BucketName, _awsConfiguration.S3Config.TypeDistinctionFile);

            var item = JsonHelper.DeserializeStream<TypeDistinction>(stream).ToList().FirstOrDefault(x => x.Type.ToLower() == type.ToLower());

            if (item == null)
                return NotFound();
            
            return Ok(item);
        }
    }
}
