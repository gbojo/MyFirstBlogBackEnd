using Microsoft.AspNetCore.Mvc;
using MyFirstBlog.Dtos;
using MyFirstBlog.Services;
using System.Collections.Generic;

namespace MyFirstBlog.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // GET /posts
        [HttpGet]
        public IEnumerable<PostDto> GetPosts()
        {
            return _postService.GetPosts();
        }

        // GET /posts/{slug}
       
        [HttpGet("{slug}")]
        public ActionResult<PostDto> GetPost(string slug)
        {
            var post = _postService.GetPost(slug);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }



        [HttpPost]
        public IActionResult CreatePost([FromBody] PostCreateRequest post)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(post.Title))
                errors.Add("Title cannot be blank");

            if (string.IsNullOrWhiteSpace(post.Description))
                errors.Add("You cannot send a message with empty description");

            if (errors.Any())
                return BadRequest(new { errors });

            var newPost = _postService.CreatePost(post);

            return CreatedAtAction(nameof(GetPost), new { slug = newPost.Slug }, new
            {
                title = newPost.Title,
                description = newPost.Description
            });
            //return CreatedAtAction(nameof(GetPost), new { slug = newPost.Slug }, newPost);
        }

    }
}
