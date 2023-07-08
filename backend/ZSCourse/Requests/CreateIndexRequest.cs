namespace ZSCourse.Requests;

public record CreateIndexRequest(string name, string description, Uri picUri, long languageId);


