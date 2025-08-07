using MyFirstBlog.Dtos;
using MyFirstBlog.Entities;
using MyFirstBlog.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public interface IPostService
{
    IEnumerable<PostDto> GetPosts();
    PostDto GetPost(string slug);
    PostDto CreatePost(PostCreateRequest post);
}


namespace MyFirstBlog.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _context;

        public PostService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<PostDto> GetPosts()
        {
            return _context.Posts.Select(post => post.AsDto());
        }

        public PostDto GetPost(string slug)
        {
            return _context.Posts.FirstOrDefault(p => p.Slug == slug)?.AsDto();
        }

        public PostDto CreatePost(PostCreateRequest post)
        {
            var entity = new Post
            {
                Title = post.Title,
                Description = post.Description,
                Slug = Regex.Replace(post.Title.ToLower(), @"\s+", "-"),
                CreatedDate = DateTime.UtcNow
            };

            _context.Posts.Add(entity);
            _context.SaveChanges(); // ✅ Save to DB

            return entity.AsDto();
        }
    }
}
