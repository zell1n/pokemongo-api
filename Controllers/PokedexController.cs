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
    public class PokedexController : Controller
    {
        private readonly IS3DataAccess _s3DataAccess;
        private readonly IAwsConfiguration _awsConfiguration;

        public PokedexController(IS3DataAccess s3DataAccess, IAwsConfiguration awsConfiguration)
        {
            _s3DataAccess = s3DataAccess;
            _awsConfiguration = awsConfiguration;
        }

        [HttpGet]
        public IActionResult GetPokedex()
        {
            var stream = _s3DataAccess.GetS3Object(_awsConfiguration.S3Config.BucketName, _awsConfiguration.S3Config.PokedexFile);

            return Ok(JsonHelper.DeserializeStream<Pokedex>(stream));
        }

        [HttpGet("name/{name}")]
        public IActionResult GetPokedexByName(string name)
        {
            var stream = _s3DataAccess.GetS3Object(_awsConfiguration.S3Config.BucketName, _awsConfiguration.S3Config.PokedexFile);

            var item = JsonHelper.DeserializeStream<Pokedex>(stream).ToList().FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

            if (item == null)
                return NotFound();
            
            return Ok(item);
        }

        [HttpGet("pokeindex/{id}")]
        public IActionResult GetPokedexByPokeIndex(int id)
        {
            var stream = _s3DataAccess.GetS3Object(_awsConfiguration.S3Config.BucketName, _awsConfiguration.S3Config.PokedexFile);

            var item = JsonHelper.DeserializeStream<Pokedex>(stream).ToList().FirstOrDefault(x => x.PokeIndex == id);

            if (item == null)
                return NotFound();
            
            return Ok(item);
        }
    }
}
