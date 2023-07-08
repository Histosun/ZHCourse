using Microsoft.EntityFrameworkCore;

namespace ZSCourse.ListeningService;

public class ListeningService : IListeningService
{
    private readonly LSDbContext dbContext;

    public ListeningService(LSDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Index[]> GetIndexByLanguageAsync(long languageId)
    {
        return await dbContext.Index.Where(index => index.LanguageId == languageId).ToArrayAsync();
    }
}
