using DotNet8.ConsoleApp.MongoDbExample;
using MongoDB.Driver;
using System.Reflection.Metadata;

public class Program
{
    public static async Task Main(string[] args)
    {
        MongoDbService dbService = new();

        //dbService.AddBlog(3, "Blog Title 3", "Blog Author 3", "Blog Content 3");

        var blogs = await dbService.GetBlogsAsync();
        foreach (var blog in blogs)
        {
            Console.WriteLine($"Blog Id: {blog.BlogId}");
            Console.WriteLine($"Blog Title: {blog.BlogTitle}");
            Console.WriteLine($"Blog Author: {blog.BlogAuthor}");
            Console.WriteLine($"Blog Content: {blog.BlogContent}");
            Console.WriteLine("------------------------------");
        }

        Console.ReadKey();
    }
}