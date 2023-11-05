using Microsoft.AspNetCore.Mvc;
using PersonalBlog.BLL.Interfaces;

namespace PersonalBlog.API.Controllers;

[ApiController]
[Route("/")]
public class CommentController : Controller
{
    private readonly ICommentService _service;
    
    public CommentController(ICommentService service)
    {
        _service = service;
    }
    
    [HttpGet("comments/{postId}")]
    public async Task<IActionResult> GetAllAsync(int postId)
    {
        var comments = await _service.GetAllAsync(postId);
        return Ok(comments);
    }
    
    [HttpGet("comment/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var comments = await _service.GetByIdAsync(id);
        return Ok(comments);
    }
    
}