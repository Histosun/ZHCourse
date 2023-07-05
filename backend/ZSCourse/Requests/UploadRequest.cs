using FluentValidation;

namespace ZSCourse.Requests;

public class UploadRequest
{
    //不要声明为Action的参数，否则不会正常工作
    public IFormFile File { get; set; }
}
public class UploadRequestValidator : AbstractValidator<UploadRequest>
{
    public UploadRequestValidator()
    {
        long maxFileSize = 50 * 1024 * 1024;
        RuleFor(e => e.File).NotNull().Must(f => f.Length > 0 && f.Length < maxFileSize);
    }
}