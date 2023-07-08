using Microsoft.AspNetCore.Identity;

namespace ZSCourse.IdentityService;

public interface ILoginService
{
    public Task<(SignInResult Result, string? Token)> LoginByUserNameAndPwdAsync(string userName, string password);
    
    public Task<bool> CreateWorldAsync();

    public Task LogoutAsnyc(string token);
}
