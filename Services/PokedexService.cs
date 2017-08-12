using System;
using System.Collections.Generic;
using System.Linq;
using pokemongo_api.DataAccess;
using pokemongo_api.Helpers;
using pokemongo_api.Models;

namespace pokemongo_api.Services
{
    public class PokedexService : IPokedexService
    {
        private readonly ITypeDistinctionService _typeDistinctionService;
        private readonly IS3DataAccess _s3DataAccess;
        private readonly IAwsConfiguration _awsConfiguration;

        public PokedexService(ITypeDistinctionService typeDistinctionService, IS3DataAccess s3DataAccess, IAwsConfiguration awsConfiguration)
        {
            _typeDistinctionService = typeDistinctionService;
            _s3DataAccess = s3DataAccess;
            _awsConfiguration = awsConfiguration;
        }

        public IEnumerable<Pokedex> GetPokedex()
        {
            var stream = _s3DataAccess.GetS3Object(_awsConfiguration.S3Config.BucketName, _awsConfiguration.S3Config.PokedexFile);

            return JsonHelper.DeserializeStream<Pokedex>(stream);
        }

        public Pokedex GetPokedexById(int pokeIndex)
        {
            return GetPokedex().FirstOrDefault(x => x.PokeIndex == pokeIndex);
        }

        public Pokedex GetPokedexByName(string name)
        {
            return GetPokedex().FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }

        public IEnumerable<Pokedex> GetTopStrongAgainst(int pokeIndex, int topCount)
        {
            var pokedex = GetPokedex();
            var pokedexTypes = pokedex.FirstOrDefault(x => x.PokeIndex == pokeIndex).Types.ToList();

            var strongTypes = new List<TypeDistinction>();
            if (pokedexTypes.Count > 1)
                strongTypes = _typeDistinctionService.GetTypeDistinctions().Where(x => x.Type == pokedexTypes.ElementAt(0) || x.Type == pokedexTypes.ElementAt(1)).ToList();
            else
                strongTypes = _typeDistinctionService.GetTypeDistinctions().Where(x => x.Type == pokedexTypes.ElementAt(0)).ToList();

            var strongPokedex = new List<Pokedex>();
            
            foreach (var type in strongTypes)
            {
                strongPokedex.AddRange(GetStrongPokedexByTypes(type, pokedex));
            }

            var topStrongestCounter = strongPokedex.OrderByDescending(x => x.MaxCP).Distinct().Take(topCount);
            return topStrongestCounter;
        }

        private IEnumerable<Pokedex> GetStrongPokedexByTypes(TypeDistinction typeDistinction, IEnumerable<Pokedex> pokedex)
        {
            var pokedexVulnerable = new List<Pokedex>();

            foreach (var type in typeDistinction.VulnerableTo)
            {
                pokedexVulnerable.AddRange(pokedex.Where(x => x.Types.Contains(type)));
            }
            return pokedexVulnerable;
        }
    }
}
