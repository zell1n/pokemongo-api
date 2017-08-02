using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pokemongo_api.Helpers;

namespace pokemongo_api.Controllers
{
    [Route("api/[controller]")]
    public class PokemonTypeController : Controller
    {
        [HttpGet]
        public IActionResult GetPokemonTypes()
        {
            return Ok(JsonHelper.ReadJsonFile<PokemonType>("Data/pokemon_types.json"));
        }

        [HttpGet("{name}")]
        public IActionResult GetPokemonTypeByPokemonName(string name)
        {
            var item = JsonHelper.ReadJsonFile<PokemonType>("Data/pokemon_types.json").ToList().FirstOrDefault(x => x.PokemonName.ToLower() == name.ToLower());
            if (item == null)
                return NotFound();
            
            return Ok(item);
        }
    }
}
