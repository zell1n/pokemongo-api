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

        public override bool Equals(object obj)
        {
            var item = obj as Pokedex;

            if (item == null)
            {
                return false;
            }

            return this.PokeIndex.Equals(item.PokeIndex);
        }

        public override int GetHashCode()
        {
            return this.PokeIndex.GetHashCode();
        }
    }
}
