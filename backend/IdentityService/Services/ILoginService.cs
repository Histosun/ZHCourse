using Microsoft.AspNetCore.Identity;

namespace ZSCourse.IdentityService.Services;

public interface ILoginService
{
    public Task<(SignInResult Result, string? Token)> LoginByUserNameAndPwdAsync(string userName, string password);
    public Task<bool> CreateWorldAsync();

    public Task Logout(string token);
}
