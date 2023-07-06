using Microsoft.AspNetCore.Mvc;

namespace ZSCourse.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class AlbumController : ControllerBase
{
   /* private readonly IListeningRepository repository;
    private readonly IMemoryCacheHelper cacheHelper;
    public AlbumController(IListeningRepository repository, IMemoryCacheHelper cacheHelper)
    {
        this.repository = repository;
        this.cacheHelper = cacheHelper;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<AlbumVM>> FindById(long id)
    {
        var album = await cacheHelper.GetOrCreateAsync($"AlbumController.FindById.{id}",
           async (e) => AlbumVM.Create(await repository.GetAlbumByIdAsync(id)));
        if (album == null)
        {
            return NotFound();
        }
        return album;
    }

    [HttpGet]
    [Route("{categoryId}")]
    public async Task<ActionResult<AlbumVM[]>> FindByCategoryId([RequiredGuid] Guid categoryId)
    {
        Task<Album[]> FindDataAsync()
        {
            return repository.GetAlbumsByCategoryIdAsync(categoryId);
        }
        var task = cacheHelper.GetOrCreateAsync($"AlbumController.FindByCategoryId.{categoryId}",
            async (e) => AlbumVM.Create(await FindDataAsync()));
        return await task;
    }*/
}