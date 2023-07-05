namespace ZSCourse.FileService;

public interface IStorageClient
{
    StorageType StorageType { get; }

    Task<Uri> SaveAsync(string key, Stream content, CancellationToken cancellationToken = default);
}
