using Microsoft.AspNetCore.Mvc;
using PersonalBlog.BLL.Subscription.Interfaces;

namespace PersonalBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _service;

        public SubscriptionController(ISubscriptionService service)
        {
            _service = service;
        }

        [HttpGet("subscriptions")]
        public async Task<IActionResult> GetSubscriptions()
        {
            var userPrincipal = HttpContext.User;
            var result = await _service.GetSubscriptions(userPrincipal);
            return Ok(result);
        }

        [HttpPost("subscribe/{blogId}")]
        public async Task<IActionResult> Subscribe(int blogId)
        {
            var userPrincipal = HttpContext.User;
            var result = await _service.Subscribe(userPrincipal, blogId);

            return Ok(result);
        }

        [HttpPost("unsubscribe/{blogId}")]
        public async Task<IActionResult> Unsubscribe(int blogId)
        {
            var userPrincipal = HttpContext.User;
            var result = await _service.Unsubscribe(userPrincipal, blogId);
            return Ok(result);
        }
    }
}
