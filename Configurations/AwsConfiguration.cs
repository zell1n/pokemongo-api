using System;

namespace pokemongo_api
{
    public class AwsConfiguration : IAwsConfiguration
    {
        public string AwsAccessKey { get; set; }
        public string AwsSecretKey { get; set; }
    }
}