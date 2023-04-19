using Microsoft.AspNetCore.Mvc;
using Minio;

namespace MinIOSample.Controllers;

[ApiController]
[Route("[controller]")]
public class BucketController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var listBucketsTask = await MinIO.Instance.ListBucketsAsync();
        return Ok(listBucketsTask);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name, CancellationToken ct)
    {
        var rq = new MakeBucketArgs()
            .WithBucket(name);

        await MinIO.Instance.MakeBucketAsync(rq, cancellationToken: ct);
        return Ok();
    }
}