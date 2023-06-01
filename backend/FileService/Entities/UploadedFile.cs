namespace ZSCourse.FileService.Entities;

public class UploadedFile
{
    public long Id { get; private set; }
    public DateTime CreationTime { get; private set; }

    /// <summary>
    /// 文件大小（尺寸为字节）
    /// </summary>
    public long FileSizeInBytes { get; private set; }
    /// <summary>
    /// Original file name
    /// </summary>
    public string FileName { get; private set; }

    /// <summary>
    /// File Hash Value (SHA256)
    /// </summary>
    public string FileSHA256Hash { get; private set; }

    /// <summary>
    /// Backup url
    /// </summary>
    public Uri BackupUrl { get; private set; }


    /// <summary>
    /// Public url
    /// </summary>
    public Uri RemoteUrl { get; private set; }

    public static UploadedFile Create(long id, long fileSizeInBytes, string fileName, string fileSHA256Hash, Uri backupUrl, Uri remoteUrl)
    {
        UploadedFile file = new UploadedFile()
        {
            Id = id,
            CreationTime = DateTime.Now,
            FileName = fileName,
            FileSHA256Hash = fileSHA256Hash,
            FileSizeInBytes = fileSizeInBytes,
            BackupUrl = backupUrl,
            RemoteUrl = remoteUrl
        };
        return file;
    }
}
