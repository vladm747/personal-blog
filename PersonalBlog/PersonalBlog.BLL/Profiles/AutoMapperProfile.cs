using AutoMapper;
using PersonalBlog.BLL.DTO;
using PersonalBlog.Common.DTO.Auth;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.BLL.Profiles;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Comment, CommentDTO>()
            .ForMember(dest => dest.UserNickName,
            opt=> opt
                .MapFrom(src => src.User!.NickName));
        CreateMap<CommentDTO, Comment>().ReverseMap();
        CreateMap<Post, PostDTO>()
            .ForMember(dest => dest.UserNickName,
                opt=> opt
                    .MapFrom(src => src.User!.NickName));
        CreateMap<PostDTO, Post>().ReverseMap();
        CreateMap<Blog, BlogDTO>()
            .ForMember(dest => dest.UserNickName,
                opt=> opt
                    .MapFrom(src => src.User!.NickName));
        CreateMap<BlogDTO, Blog>();
        
        CreateMap<User, UserDTO>().ReverseMap();
    }
}