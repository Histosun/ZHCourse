using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ZSCourse.IdentityService.Requests;
using ZSCourse.IdentityService.Services;

namespace ZSCourse.IdentityService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize(AuthenticationSchemes = "Schema")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService idService;
        public LoginController(ILoginService idService)
        {
            this.idService = idService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CreateWorld()
        {
            if (await idService.CreateWorldAsync())
                return StatusCode((int)HttpStatusCode.Conflict, "Already Initialized");
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> LoginByUserNameAndPwd(LoginByUserNameAndPwdRequest req)
        {
            (var checkResult, var token) = await idService.LoginByUserNameAndPwdAsync(req.UserName, req.Password);
            if (checkResult.Succeeded) return token!;
            else if (checkResult.IsLockedOut)
                return StatusCode((int)HttpStatusCode.Locked, "User has been locked!");
            string msg = checkResult.ToString();
            return BadRequest("Login failed: " + msg);
        }

        [HttpPost]
        public ActionResult Logout(LogoutRequest logout)
        {
            idService.Logout(logout.Token);
            return Ok();
        }

        [HttpGet]
        public void Test()
        {

        }
    }
}