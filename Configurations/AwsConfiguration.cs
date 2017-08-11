using System;

namespace pokemongo_api
{
    public class AwsConfiguration : IAwsConfiguration
    {
        public string AwsAccessKey { get; set; }
        public string AwsSecretKey { get; set; }
        public S3Config S3Config { get; set; }
    }

    public class S3Config
    {
        public string BucketName { get; set; }
        public string PokedexFile { get; set; }
        public string TypeDistinctionFile { get; set; }
    }
}