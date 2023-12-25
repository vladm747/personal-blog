using Microsoft.AspNetCore.Mvc;
using PersonalBlog.BLL.Interfaces.Auth;

namespace PersonalBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        [HttpGet("me")] 
        public IActionResult GetCurrentUser()
        {
            var result = _service.GetCurrentUser(HttpContext.User);
            
            return Ok(result);
        }
        
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }
        
        [HttpDelete("user")]
        public async Task<IActionResult> DeleteAccount(string email)
        {
            var result = await _service.DeleteAsync(HttpContext.User, email);
        
            switch (result.Succeeded)
            {
                case true:
                    return Ok();
                case false:
                    return BadRequest(result.Errors.ToString());
            }
        } 
    }
}
