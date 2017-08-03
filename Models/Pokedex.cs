using System.Collections.Generic;

namespace pokemongo_api.Models
{
    public class Pokedex
    {
        public string Name { get; set; }
        public int PokeIndex { get; set; }
        public int HP { get; set;}
        public int Attack { get; set; }
        public int Defense { get; set; }
        public double MinCP { get; set; }
        public double MaxCP { get; set; }
        public IEnumerable<string> Types { get; set; }
    }
}
