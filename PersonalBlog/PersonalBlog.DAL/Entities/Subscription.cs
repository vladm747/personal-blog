using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PersonalBlog.DAL.Entities;

public class Subscription
{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; }
    public int BlogId { get; set; }
}