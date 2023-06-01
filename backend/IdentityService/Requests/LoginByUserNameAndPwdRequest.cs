namespace ZSCourse.IdentityService.Requests;

public record LoginByUserNameAndPwdRequest(string UserName, string Password);
