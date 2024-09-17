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
            //await ReadById(1);
            //await Insert(3, "Blog Title 3", "Blog Author 3", "Blog Content 3");
            //await Update(1, "Blog Title 1 updated", "Blog Author 1 updated", "Blog Content 1 updated");
            await ReadAll();
        }

        public async Task ReadAll()
        {
            var blogs = await GetBlogsAsync();
            foreach (var blog in blogs)
            {
                Console.WriteLine($"Id: {blog.Id}");
                Console.WriteLine($"Blog Id: {blog.BlogId}");
                Console.WriteLine($"Blog Title: {blog.BlogTitle}");
                Console.WriteLine($"Blog Author: {blog.BlogAuthor}");
                Console.WriteLine($"Blog Content: {blog.BlogContent}");
                Console.WriteLine("-------------------------------");
            }
        }

        public async Task ReadById(int id)
        {
            var blog = await GetBlogByIdAsync(id);
            Console.WriteLine($"Id: {blog.Id}");
            Console.WriteLine($"Blog Id: {blog.BlogId}");
            Console.WriteLine($"Blog Title: {blog.BlogTitle}");
            Console.WriteLine($"Blog Author: {blog.BlogAuthor}");
            Console.WriteLine($"Blog Content: {blog.BlogContent}");
        }

        public async Task Insert(int id, string blogTitle, string blogAuthor, string blogContent)
        {
            await AddBlogAsync(id, blogTitle, blogAuthor, blogContent);
        }

        public async Task Update(int id, string blogTitle, string blogAuthor, string blogContent)
        {
            await UpdateBlogAsync(id, blogTitle, blogAuthor, blogContent);
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

        public async Task AddBlogAsync(int id, string blogTitle, string blogAuthor, string blogContent)
        {
            var blog = new BlogModel()
            {
                BlogId = id,
                BlogTitle = blogTitle,
                BlogAuthor = blogAuthor,
                BlogContent = blogContent
            };
            var collection = GetCollection();
            await collection.InsertOneAsync(blog);
        }

        public async Task UpdateBlogAsync(int id, string blogTitle, string blogAuthor, string blogContent)
        {
            var blog = await GetBlogByIdAsync(id);
            ArgumentNullException.ThrowIfNull(blog);

            var collection = GetCollection();
            var updateDefinition = Builders<BlogModel>.Update
                .Set(b => b.BlogTitle, blogTitle)
                .Set(b => b.BlogAuthor, blogAuthor)
                .Set(b => b.BlogContent, blogContent);

            await collection.UpdateOneAsync(
                b => b.BlogId == id,
                updateDefinition
                );
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
