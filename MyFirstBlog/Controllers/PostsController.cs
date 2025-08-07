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

        // POST /posts (no DB persistence — only echoes back for now)
        [HttpPost]
        public IActionResult CreatePost([FromBody] PostCreateRequest post)
        {
            if (string.IsNullOrWhiteSpace(post.Title))
            {
                return BadRequest(new { errors = new[] { "Title cannot be blank" } });
            }

            var newPost = _postService.CreatePost(post); //save to DB
            return CreatedAtAction(nameof(GetPost), new { slug = newPost.Slug }, newPost);

            var response = new
            {
                post = new
                {
                    title = post.Title,
                    description = post.Description
                }
            };

            return Created("", response);
        }
    }
}
