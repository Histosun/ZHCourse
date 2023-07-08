namespace ZSCourse.Requests;

public record CreateIndexRequest(string name, string title, Uri picUri, long languageId);