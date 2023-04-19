using Minio;

namespace MinIOSample;

public static class MinIO
{
    const string Endpoint = "localhost:9000";
    const string AccessKey = "minioadmin";
    const string SecretKey = "minioadmin";
    const bool Secure = false;

    public static readonly MinioClient Instance = new MinioClient()
        .WithEndpoint(Endpoint)
        .WithCredentials(AccessKey, SecretKey)
        .WithSSL(Secure)
        .Build();
}
