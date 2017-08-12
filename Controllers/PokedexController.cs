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
    public class PokedexController : Controller
    {
        private readonly IPokedexService _pokedexService;

        public PokedexController(IPokedexService pokedexService)
        {
            _pokedexService = pokedexService;
        }

        [HttpGet]
        public IActionResult GetPokedex()
        {
            return Ok(_pokedexService.GetPokedex());
        }

        [HttpGet("{id}")]
        public IActionResult GetPokedexByPokeIndex(int id)
        {
            var item = _pokedexService.GetPokedexById(id);

            if (item == null)
                return NotFound();
            
            return Ok(item);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetPokedexByName(string name)
        {
            var item = _pokedexService.GetPokedexByName(name);

            if (item == null)
                return NotFound();
            
            return Ok(item);
        }

        [HttpGet("strongAgainst/{id}")]
        public IActionResult GetTopPokedexStrongAgainst(int id)
        {
            return Ok(_pokedexService.GetTopStrongAgainst(id, 5));
        }
    }
}
