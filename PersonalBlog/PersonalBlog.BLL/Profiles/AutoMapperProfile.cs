using AutoMapper;
using PersonalBlog.BLL.DTO;
using PersonalBlog.BLL.DTO.Auth;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.BLL.Profiles;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Comment, CommentDTO>().ReverseMap();
        CreateMap<Post, PostDTO>().ReverseMap();
        CreateMap<Blog, BlogDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
    }
}