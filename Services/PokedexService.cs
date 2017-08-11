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
        private readonly IS3DataAccess _s3DataAccess;
        private readonly IAwsConfiguration _awsConfiguration;

        public PokedexService(IS3DataAccess s3DataAccess, IAwsConfiguration awsConfiguration)
        {
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
            var stream = _s3DataAccess.GetS3Object(_awsConfiguration.S3Config.BucketName, _awsConfiguration.S3Config.PokedexFile);

            return JsonHelper.DeserializeStream<Pokedex>(stream).ToList().FirstOrDefault(x => x.PokeIndex == pokeIndex);
        }

        public Pokedex GetPokedexByName(string name)
        {
            var stream = _s3DataAccess.GetS3Object(_awsConfiguration.S3Config.BucketName, _awsConfiguration.S3Config.PokedexFile);

            return JsonHelper.DeserializeStream<Pokedex>(stream).ToList().FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }

        public IEnumerable<Pokedex> GetTopStrongAgainst(int pokeIndex, int topCount)
        {
            throw new NotImplementedException();
        }
    }
}
