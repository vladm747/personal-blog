using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PersonalBlog.DAL.Entities;

public class Subscription
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)] 
    public string UserId { get; set; } = string.Empty; 
    public int BlogId { get; set; }
}