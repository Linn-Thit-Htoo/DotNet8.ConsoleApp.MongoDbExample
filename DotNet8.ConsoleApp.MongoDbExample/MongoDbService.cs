using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.ConsoleApp.MongoDbExample
{
    public class MongoDbService
    {
        private IMongoCollection<BlogModel> GetCollection()
        {
            var client = new MongoClient("mongodb://localhost:27017/");
            var database = client.GetDatabase("testDb");
            var collection = database.GetCollection<BlogModel>("Blogs");

            return collection;
        }
        public void AddBlog(int id, string blogTitle, string blogAuthor, string blogContent)
        {
            var blog = new BlogModel()
            {
                BlogId = id,
                BlogTitle = blogTitle,
                BlogAuthor = blogAuthor,
                BlogContent = blogContent
            };
            var collection = GetCollection();
            collection.InsertOne(blog);
        }

        public async Task<List<BlogModel>> GetBlogsAsync()
        {
            var collection = GetCollection();
            var blogs = await collection.Find(x => true).ToListAsync();

            return blogs;
        }
    }
}
