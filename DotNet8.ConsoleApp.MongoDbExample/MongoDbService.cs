using MongoDB.Bson;
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
        public async Task Run()
        {
            await GetBlogByIdAsync(1);

            //AddBlog(3, "Blog Title 3", "Blog Author 3", "Blog Content 3");

            //var blogs = await GetBlogsAsync();
            //foreach (var blog in blogs)
            //{
            //    Console.WriteLine($"Blog Id: {blog.Id}");
            //    Console.WriteLine($"Blog Title: {blog.BlogTitle}");
            //    Console.WriteLine($"Blog Author: {blog.BlogAuthor}");
            //    Console.WriteLine($"Blog Content: {blog.BlogContent}");
            //    Console.WriteLine("-------------------------------");
            //}
        }

        public async Task<List<BlogModel>> GetBlogsAsync()
        {
            var collection = GetCollection();
            var blogs = await collection.Find(x => true).ToListAsync();

            return blogs;
        }

        public async Task<BlogModel> GetBlogByIdAsync(int id)
        {
            var collection = GetCollection();
            var blog = await collection.Find(x => x.BlogId == id).FirstOrDefaultAsync();

            return blog;
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

        private IMongoCollection<BlogModel> GetCollection()
        {
            var client = new MongoClient("mongodb://localhost:27017/");
            var database = client.GetDatabase("testDb");
            var collection = database.GetCollection<BlogModel>("Blogs");

            return collection;
        }
    }
}
