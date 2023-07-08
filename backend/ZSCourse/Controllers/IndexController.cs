using ZSCourse.ListeningService;
using Microsoft.AspNetCore.Mvc;

namespace ZSCourse.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class IndexController : ControllerBase
{
    private readonly IListeningService listeningService;

    public IndexController(IListeningService listeningService)
    {
        this.listeningService = listeningService;
    }


    [HttpGet]
    public async Task<ListeningService.Index[]> GetIndexByLanguage(long languageId)
    {
        return await listeningService.GetIndexByLanguageAsync(languageId);
    }
}