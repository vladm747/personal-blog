﻿using AutoMapper;
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