public class Program
{
    public static async Task Main(string[] args)
    {
        MongoDbService dbService = new();
        await dbService.Run();

        Console.ReadKey();
    }
}
