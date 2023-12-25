using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Common.DTO;
using PersonalBlog.BLL.Interfaces;

namespace PersonalBlog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    [Authorize]
    [HttpPost("comment")]
    public async Task<IActionResult> CreateComment([FromBody] CommentDTO comment)
    {
        await _service.CreateAsync(comment);
        return Ok();
    }
    [Authorize]
    [HttpDelete("comment/{commentId}")]
    public async Task<IActionResult> DeleteAsync(int commentId)
    {
        await _service.DeleteAsync(commentId);
        return Ok();
    }
    [Authorize]
    [HttpPut("comment")]
    public async Task<IActionResult> UpdateAsync(int id, CommentDTO comment)
    {
        if (id != comment.Id)
            return BadRequest();
        await _service.UpdateAsync(comment);
        return Ok();
    }
}