using AutoMapper;
using PersonalBlog.BLL.DTO;
using PersonalBlog.DAL.Entities;

namespace PersonalBlog.BLL.Profiles;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Comment, CommentDTO>();
        // .ForMember(
        //     dest => dest.Id,
        //     opt => opt.MapFrom(src => src.Id)
        // )
        // .ForMember(
        //     dest => dest.Content,
        //     opt => opt.MapFrom(src => src.Content)
        // )
        // .ForMember(
        //     dest => dest.UserId,
        //     opt => opt.MapFrom(src => src.UserId)
        // )
        // .ForMember(
        //     dest => dest.CommentDate,
        //     opt => opt.MapFrom(src => src.CommentDate)
        // )
        // .ForMember(
        //     dest => dest.PostId,
        //     opt => opt.MapFrom(src => src.PostId)
        // );
    }
}