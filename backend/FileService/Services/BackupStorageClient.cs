using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ZSCourse.FileService;

namespace FileService.Services
{
    public class BackupStorageClient : IStorageClient
    {
        public StorageType StorageType => StorageType.Backup;
        private IOptionsSnapshot<BackupStorageOptions> options;
        public BackupStorageClient(IOptionsSnapshot<BackupStorageOptions> options)
        {
            this.options = options;
        }

        public async Task<Uri> SaveAsync(string key, Stream content, CancellationToken cancellationToken = default)
        {
            if (key.StartsWith('/'))
            {
                throw new ArgumentException("key should not start with /", nameof(key));
            }
            string workingDir = options.Value.WorkingDir;
            string fullPath = Path.Combine(workingDir, key);
            string? fullDir = Path.GetDirectoryName(fullPath);//get the directory
            if (!Directory.Exists(fullDir))//automatically create dir
            {
                Directory.CreateDirectory(fullDir);
            }
            if (File.Exists(fullPath))//如果已经存在，则尝试删除
            {
                File.Delete(fullPath);
            }
            using Stream outStream = File.OpenWrite(fullPath);
            await content.CopyToAsync(outStream, cancellationToken);
            return new Uri(fullPath);

        }
    }
}
