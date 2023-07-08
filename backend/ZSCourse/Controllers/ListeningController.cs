using ZSCourse.ListeningService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ZS.JWT;

namespace ZSCourse.Controllers;

[Route("[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = ZSJWTDefaults.Schema)]
public class ListeningController : ControllerBase
{
    private readonly IListeningService listeningService;

    public ListeningController(IListeningService listeningService)
    {
        this.listeningService = listeningService;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<ListeningService.Index[]> CreateIndex([FromBody] long languageId)
    {
        return await listeningService.GetIndexByLanguageAsync(languageId);
    }


    [HttpGet]
    [AllowAnonymous]
    public async Task<ListeningService.Index[]> GetIndexByLanguage([FromQuery]long languageId)
    {
        return await listeningService.GetIndexByLanguageAsync(languageId);
    }
}