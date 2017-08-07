using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pokemongo_api.DataAccess;
using pokemongo_api.Helpers;
using pokemongo_api.Models;

namespace pokemongo_api.Controllers
{
    [Route("api/[controller]")]
    public class PokedexController : Controller
    {
        [HttpGet]
        public IActionResult GetPokedex()
        {
            S3DataAccess s3 = new S3DataAccess();
            var stream = s3.GetS3Object();

            return Ok(JsonHelper.DeserializeStream(stream));
        }

        [HttpGet("{name}")]
        public IActionResult GetPokedexByName(string name)
        {
            S3DataAccess s3 = new S3DataAccess();
            var stream = s3.GetS3Object();

            var item = JsonHelper.DeserializeStream(stream).ToList().FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

            if (item == null)
                return NotFound();
            
            return Ok(item);
        }
    }
}
