using System.Collections.Generic;

namespace pokemongo_api.Models
{
    public class PokemonType
    {
        public string PokemonName { get; set; }
        public IEnumerable<string> Types { get; set; }
    }
}
