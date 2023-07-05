using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZS.JWT;
using ZSCourse.FileService;
using ZSCourse.Requests;

namespace ZSCourse.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize(AuthenticationSchemes = ZSJWTDefaults.Schema, Roles = "Admin")]
    public class FileController
    {
        private readonly IFileService FileService;

        public FileController(IFileService fileService)
        {
            FileService = fileService;
        }

        [HttpGet]
        public async Task<FileExistsResponse> FileExists(long fileSize, string sha256Hash)
        {
            var item = await FileService.FindFileAsync(fileSize, sha256Hash);

            if (item == null)
                return new FileExistsResponse(false, null);

            return new FileExistsResponse(true, item.RemoteUrl);
        }

        [HttpPost]
        [RequestSizeLimit(60_000_000)]
        public async Task<ActionResult<Uri>> Upload([FromForm] UploadRequest request, CancellationToken cancellationToken = default)
        {
            var file = request.File;
            string fileName = file.FileName;
            using Stream stream = file.OpenReadStream();
            var upFile = await FileService.UploadAsync(stream, fileName, cancellationToken);
            return upFile.RemoteUrl;
        }
    }
}
