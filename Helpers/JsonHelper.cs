using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace pokemongo_api.Helpers
{
    public static class JsonHelper
    {
        public static IEnumerable<T> ReadJsonFile<T>(string input)
        {
            var json = File.ReadAllText(@input);

            var items = JsonConvert.DeserializeObject<List<T>>(json);

            return items;
        }
    }
}
