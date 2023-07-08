using ZSCourse.ListeningService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ZS.JWT;

namespace ZSCourse.Controllers;

[Route("[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = ZSJWTDefaults.Schema)]
public class IndexController : ControllerBase
{
    private readonly IListeningService listeningService;

    public IndexController(IListeningService listeningService)
    {
        this.listeningService = listeningService;
    }


    [HttpGet]
    [AllowAnonymous]
    public async Task<ListeningService.Index[]> GetIndexByLanguage([FromQuery]long languageId)
    {
        return await listeningService.GetIndexByLanguageAsync(languageId);
    }
}