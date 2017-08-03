using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public static IEnumerable<PokemonType> Parser(string inputFile)
        {
            List<PokemonType> listOfPokemonTypes = new List<PokemonType>();

            var lines = File.ReadLines(inputFile);

            foreach (var line in lines)
            {
                var split = line.Split('\t');

                var listOfTypes = new List<string>();
                if (split[2] == string.Empty)
                    listOfTypes.Add(split[1]);
                else
                {
                    listOfTypes.Add(split[1]);
                    listOfTypes.Add(split[2]);
                }

                listOfPokemonTypes.Add(
                    new PokemonType
                    {
                        PokemonName = split[0],
                        Types = listOfTypes
                    }
                );
            }

            return listOfPokemonTypes;
        }

        public static IEnumerable<Pokedex> CombineJsonFiles(string pokemonsFile, string pokemonTypesFile)
        {
            var pokemons = ReadJsonFile<Pokedex>(pokemonsFile);
            var types = ReadJsonFile<PokemonType>(pokemonTypesFile);

            foreach (var type in types)
            {
                var pokemon = pokemons.ToList().FirstOrDefault(x => x.Name == type.PokemonName);
                pokemon.Types = type.Types;
            }
            return pokemons;
        }
    }
}
