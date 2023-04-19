using Microsoft.AspNetCore.Mvc;
using Minio;
using MinIOSample.Models;

namespace MinIOSample.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ObjectController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string bucketName, [FromQuery] string objectName, CancellationToken ct)
    {
        using var filestream = new MemoryStream();

        var rq = new GetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithCallbackStream(async stream => await stream.CopyToAsync(filestream));

        var rs = await MinIO.Instance.GetObjectAsync(rq, ct);

        return File(filestream, rs.ContentType);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateObjectModel model, CancellationToken ct)
    {
        using var filestream = new MemoryStream();
        await model.Object.CopyToAsync(filestream, ct);

        var metaData = new Dictionary<string, string>
        {
            { "FullName", "Farshad Goodarzi" },
        };

        var args = new PutObjectArgs()
            .WithBucket(model.BucketName)
            .WithObject(model.ObjectName)
            .WithStreamData(filestream)
            .WithObjectSize(model.Object.Length)
            .WithContentType(model.Object.ContentType)
            .WithHeaders(metaData);

        await MinIO.Instance.PutObjectAsync(args, ct);

        return Ok();
    }
}