namespace pokemongo_api
{
    public interface IAwsConfiguration
    {
        string AwsAccessKey { get; set; }
        string AwsSecretKey { get; set; }
        S3Config S3Config { get; set; }
    }
}