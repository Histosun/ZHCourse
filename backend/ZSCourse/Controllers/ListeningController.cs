using ZSCourse.ListeningService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ZS.JWT;
using ZSCourse.Requests;

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

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task CreateIndex([FromBody] CreateIndexRequest request)
    {
        var index = new ListeningService.Index();
        index.Title = request.title;
        index.coverPicUri = request.picUri;
        index.Title = request.title;
        index.LanguageId = request.languageId;
        await listeningService.CreateIndexAsync(index);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ListeningService.Index[]> GetIndexByLanguage([FromQuery]long languageId)
    {
        return await listeningService.GetIndexByLanguageAsync(languageId);
    }
}