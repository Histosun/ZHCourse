namespace ZSCourse.ListeningService;

public interface IListeningService
{
    public Task<Index[]> GetIndexByLanguageAsync(long languageId);
    public Task CreateIndexAsync(Index index);
}
