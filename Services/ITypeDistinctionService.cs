using System.Collections.Generic;
using pokemongo_api.Models;

namespace pokemongo_api.Services
{
    public interface ITypeDistinctionService
    {
        IEnumerable<TypeDistinction> GetTypeDistinctions();
        TypeDistinction GetTypeDistinctionByType(string type);
    }
}