using AutoMapper;
using PersonalBlog.BLL.DTO;
using PersonalBlog.DAL.Entities;

namespace PersonalBlog.BLL.Profiles;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Comment, CommentDTO>().ReverseMap();
        CreateMap<Post, PostDTO>().ReverseMap();
        CreateMap<Blog, BlogDTO>().ReverseMap();
    }
}