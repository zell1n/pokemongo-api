using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using pokemongo_api.Models;

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

        public static IEnumerable<T> DeserializeStream<T>(MemoryStream memoryStream)
        {
            var jsonString = Encoding.ASCII.GetString(memoryStream.ToArray());

            var items = JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            return items;
        }
    }
}
