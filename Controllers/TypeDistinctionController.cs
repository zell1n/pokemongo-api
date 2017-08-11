using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using pokemongo_api.DataAccess;
using pokemongo_api.Helpers;
using pokemongo_api.Models;
using pokemongo_api.Services;

namespace pokemongo_api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class TypeDistinctionController : Controller
    {
        private readonly ITypeDistinctionService _typeDistinctionService;

        public TypeDistinctionController(ITypeDistinctionService typeDistinctionService)
        {
            _typeDistinctionService = typeDistinctionService;
        }

        [HttpGet]
        public IActionResult GetTypeDistinctions()
        {
            return Ok(_typeDistinctionService.GetTypeDistinctions());
        }

        [HttpGet("type/{type}")]
        public IActionResult GetTypeDistinctionByType(string type)
        {
            var item = _typeDistinctionService.GetTypeDistinctionByType(type);

            if (item == null)
                return NotFound();
            
            return Ok(item);
        }
    }
}
