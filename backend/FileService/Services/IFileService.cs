namespace ZSCourse.FileService;

public interface IFileService
{
    Task<UploadedFile> FindFileAsync(long fileSize, string sha256Hash);
    Task<UploadedFile> UploadAsync(Stream stream, string fileName, CancellationToken cancellationToken);
}
