using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Common.DTO;
using PersonalBlog.BLL.Interfaces;

namespace PersonalBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController: Controller
    {
        private readonly IPostService _service;
    
        public PostController(IPostService service)
        {
            _service = service;
        }
    
        [HttpGet("posts/{blogId}")]
        public async Task<IActionResult> GetAllAsync(int blogId)
        {
            var posts = await _service.GetAllAsync(blogId);
            return Ok(posts);
        }
    
        [HttpGet("post/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var posts = await _service.GetByIdAsync(id);
            return Ok(posts);
        }
        
        [Authorize(Roles = "author")]
        [HttpPost("post")]
        public async Task<IActionResult> CreatePost([FromBody] PostDTO post)
        {
            var postDto = await _service.CreateAsync(post);
       
            return Ok(postDto);
        }
        
        [Authorize(Roles = "author")]
        [HttpPut("post")]
        public async Task<IActionResult> UpdateAsync(int id, PostDTO post)
        {
            if (id != post.Id)
                return BadRequest();
            var result = await _service.UpdateAsync(post);
            
            return Ok(result);
        }
        
        [Authorize(Roles = "author")]
        [HttpDelete("post/{postId}")]
        public async Task<IActionResult> DeleteAsync(int postId)
        {
            var result = await _service.DeleteAsync(postId);
            
            return Ok(result);
        }
    }
}
