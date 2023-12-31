using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.BLL.Interfaces;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        private readonly IBlogService _service;
        private readonly UserManager<User> _userManager;
        
        public BlogController(IBlogService service, UserManager<User> userManager)
        {
            _service = service;
            _userManager = userManager;
        }
       
        [HttpGet("blogs")]
        public async Task<IActionResult> GetAllAsync()
        {
            var blogs = await _service.GetAllAsync();

            return Ok(blogs);
        }
        
        [HttpGet("blog/{blogId}")]
        public async Task<IActionResult> GetByIdAsync(int blogId)
        {
            var blog = await _service.GetByIdAsync(blogId);

            return Ok(blog);
        }
        
        [Authorize(Roles = "author")]
        [HttpPost("blog")]
        public async Task<IActionResult> CreateAsync([FromBody] string userId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user.Id != userId)
                return BadRequest();

            var result = await _service.CreateAsync(userId);
            return Ok(result);
        }
        [Authorize(Roles = "author")]
        [HttpDelete("blog/{blogId}")]
        public async Task<IActionResult> DeleteAsync(int blogId)
        {
            var result = await _service.DeleteAsync(blogId);
            
            return Ok(result);
        }
    }
}
