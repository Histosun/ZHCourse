using ListeningService.Services;
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
    public Task<IEnumerable<ListeningService.Index>> GetIndexByLanguage()
    {
        
        return null;
    }
}