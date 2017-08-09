using System.IO;

namespace pokemongo_api.DataAccess
{
    public interface IS3DataAccess
    {
        MemoryStream GetS3Object();
        
    }
}