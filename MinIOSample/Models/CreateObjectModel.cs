namespace MinIOSample.Models;

public sealed class CreateObjectModel
{
    public string BucketName { get; set; } = default!;
    public string ObjectName { get; set; } = default!;
    public IFormFile Object { get; set; } = default!;
}
