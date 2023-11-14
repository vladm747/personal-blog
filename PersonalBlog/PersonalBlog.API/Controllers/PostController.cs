using Microsoft.AspNetCore.Mvc;
using PersonalBlog.BLL.DTO;
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

        [HttpPost("post")]
        public async Task<IActionResult> CreateComment([FromBody] PostDTO post)
        {
            var userPrincipal = HttpContext.User;
            await _service.CreateAsync(userPrincipal, post);
            return Ok();
        }
        
        [HttpPut("post")]
        public async Task<IActionResult> UpdateAsync(int id, PostDTO post)
        {
            if (id != post.Id)
                return BadRequest();
            await _service.UpdateAsync(post);
            return Ok();
        }
        
        [HttpDelete("post/{postId}")]
        public async Task<IActionResult> DeleteAsync(int postId)
        {
            await _service.DeleteAsync(postId);
            return Ok();
        }
    }
}
