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
        
        [HttpPost("blog")]
        public async Task<IActionResult> CreateAsync(string userId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user.Id != userId)
                return BadRequest();

            await _service.CreateAsync(userId);
            return Ok();
        }

        [HttpDelete("blog/{blogId}")]
        public async Task<IActionResult> DeleteAsync(int blogId)
        {
            await _service.DeleteAsync(blogId);
            return Ok();
        }
    }
}
