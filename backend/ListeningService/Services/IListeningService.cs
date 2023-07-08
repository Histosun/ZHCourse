namespace ZSCourse.ListeningService;

public interface IListeningService
{
    public Task<Index[]> GetIndexByLanguageAsync(long languageId);
}
