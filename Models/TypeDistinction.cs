using System.Collections.Generic;

namespace pokemongo_api.Models
{
    public class TypeDistinction
    {
        public string Type { get; set; }
        public IEnumerable<string> StrongAgainst  { get; set; }
        public IEnumerable<string> VulnerableTo  { get; set; }
    }
}
