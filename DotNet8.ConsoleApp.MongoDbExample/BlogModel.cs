namespace DotNet8.ConsoleApp.MongoDbExample;

public class BlogModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    public int BlogId { get; set; }
    public string BlogTitle { get; set; }
    public string BlogAuthor { get; set; }
    public string BlogContent { get; set; }
}
