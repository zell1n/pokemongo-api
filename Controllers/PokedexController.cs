using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using pokemongo_api.DataAccess;
using pokemongo_api.Helpers;
using pokemongo_api.Models;

namespace pokemongo_api.Controllers
{
    [Route("api/[controller]")]
    public class PokedexController : Controller
    {
        private readonly IS3DataAccess _s3DataAccess;

        public PokedexController(IS3DataAccess s3DataAccess)
        {
            _s3DataAccess = s3DataAccess;
        }

        [HttpGet]
        public IActionResult GetPokedex()
        {
            var stream = _s3DataAccess.GetS3Object();

            return Ok(JsonHelper.DeserializeStream(stream));
        }

        [HttpGet("{name}")]
        public IActionResult GetPokedexByName(string name)
        {
            var stream = _s3DataAccess.GetS3Object();

            var item = JsonHelper.DeserializeStream(stream).ToList().FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

            if (item == null)
                return NotFound();
            
            return Ok(item);
        }
    }
}
