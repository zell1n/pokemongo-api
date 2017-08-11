using System.Collections.Generic;
using pokemongo_api.Models;

namespace pokemongo_api.Services
{
    public interface IPokedexService
    {
        IEnumerable<Pokedex> GetPokedex();
        Pokedex GetPokedexById(int pokeIndex);
        Pokedex GetPokedexByName(string name);
        IEnumerable<Pokedex> GetTopStrongAgainst(int pokeIndex, int topCount);
    }
}