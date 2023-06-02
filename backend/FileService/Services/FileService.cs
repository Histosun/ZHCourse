using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace ZSCourse.FileService;

public class FileService : IFileService
{
    private readonly FSDbContext DbContext;
    private readonly IStorageClient BackupStorage;

    public FileService(FSDbContext dbContext, IStorageClient storageClient)
    {
        DbContext = dbContext;
        BackupStorage = storageClient;
    }
    public Task<UploadedFile> FindFileAsync(long fileSize, string sha256Hash)
    {
        return DbContext.UploadedFile
            .Where(file => file.FileSHA256Hash == sha256Hash && file.FileSizeInBytes == fileSize)
            .FirstAsync();
    }

    public async Task<UploadedFile> UploadAsync(Stream stream, string fileName, CancellationToken cancellationToken)
    {
        var sha256Hash = ComputeSha256Hash(stream);
        var fileSize = stream.Length;
        var oldFile = await FindFileAsync(fileSize, sha256Hash);
        
        if(oldFile != null)
            return oldFile;


        DateTime today = DateTime.Today;
        string key = $"{today.Year}/{today.Month}/{today.Day}/{sha256Hash}/{fileName}";
        
        stream.Position = 0;
        Uri backupUrl = await BackupStorage.SaveAsync(key, stream, cancellationToken);

        var upFile = UploadedFile.Create(fileSize, fileName, sha256Hash, backupUrl, backupUrl);
        DbContext.Add(upFile);
        await DbContext.SaveChangesAsync();

        return upFile;
    }

    private static string ComputeSha256Hash(Stream stream)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(stream);
            return ToHashString(bytes);
        }
    }
    private static string ToHashString(byte[] bytes)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
    }
}
