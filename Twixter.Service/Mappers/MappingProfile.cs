using AutoMapper;
using Twixter.Models.Dtos.Comments;
using Twixter.Models.Dtos.Posts;
using Twixter.Models.Dtos.Users;
using Twixter.Models.Entities;

namespace Twixter.Service.Mappers;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequestDto, ApplicationUser>();
        CreateMap<ApplicationUser, UserResponseDto>();
        
        CreateMap<Post,PostResponseDto>();
        CreateMap<PostAddRequestDto, Post>();
        CreateMap<PostResponseDto, PostAddRequestDto>();

        CreateMap<Comment, CommentResponseDto>();
        CreateMap<CommentAddRequestDto, Comment>();
    }
}