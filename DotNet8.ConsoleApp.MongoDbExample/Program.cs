using DotNet8.ConsoleApp.MongoDbExample;
using MongoDB.Driver;
using System.Reflection.Metadata;

public class Program
{
    public static async Task Main(string[] args)
    {
        MongoDbService dbService = new();
        await dbService.Run();

        Console.ReadKey();
    }
}