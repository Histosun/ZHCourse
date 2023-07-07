using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ZSCourse.FileService;

/// <summary>
/// Consider Our backend Server as a cloud storage server, which serves as a mock. Files are stored in the wwwroot directory.
/// This is intended for development and demonstration purposes only. In a production environment, it is crucial to replace it with a dedicated cloud storage server.
/// </summary>
class MockCloudStorageClient : IStorageClient
{
    public StorageType StorageType => StorageType.Public;
    private readonly IWebHostEnvironment hostEnv;
    private readonly IHttpContextAccessor httpContextAccessor;

    public MockCloudStorageClient(IWebHostEnvironment hostEnv, IHttpContextAccessor httpContextAccessor)
    {
        this.hostEnv = hostEnv;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<Uri> SaveAsync(string key, Stream content, CancellationToken cancellationToken = default)
    {
        if (key.StartsWith('/'))
        {
            throw new ArgumentException("key should not start with /", nameof(key));
        }
        string workingDir = Path.Combine(hostEnv.ContentRootPath, "wwwroot");
        string fullPath = Path.Combine(workingDir, key);
        string? fullDir = Path.GetDirectoryName(fullPath);//get the directory
        if (!Directory.Exists(fullDir))//automatically create dir
        {
            Directory.CreateDirectory(fullDir);
        }
        if (File.Exists(fullPath))//if exits delete first
        {
            File.Delete(fullPath);
        }
        using Stream outStream = File.OpenWrite(fullPath);
        await content.CopyToAsync(outStream, cancellationToken);
        var req = httpContextAccessor.HttpContext.Request;
        string url = req.Scheme + "://" + req.Host + "/FileService/" + key;
        return new Uri(url);
    }
}