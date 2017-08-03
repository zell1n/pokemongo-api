using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(JsonHelper.ReadJsonFile<Pokedex>("Data/pokemons_api.json"));
        }

        [HttpGet("{name}")]
        public IActionResult GetPokedexByName(string name)
        {
            var item = JsonHelper.ReadJsonFile<Pokedex>("Data/pokemons_api.json").ToList().FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            if (item == null)
                return NotFound();
            
            return Ok(item);
        }
    }
}
