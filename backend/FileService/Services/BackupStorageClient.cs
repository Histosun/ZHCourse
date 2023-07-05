using ZSCourse.FileService;

namespace FileService.Services
{
    public class BackupStorageClient : IStorageClient
    {
        public StorageType StorageType => StorageType.Backup;

        public Task<Uri> SaveAsync(string key, Stream content, CancellationToken cancellationToken = default)
        {
            if (key.StartsWith('/'))
            {
                throw new ArgumentException("key should not start with /", nameof(key));
            }

        }
    }
}
