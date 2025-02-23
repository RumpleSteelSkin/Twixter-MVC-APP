using AutoMapper;
using Twixter.Models.Dtos.Comments;
using Twixter.Models.Entities;
using Twixter.Repository.Repositories.Abstracts;
using Twixter.Service.Services.Abstracts;

namespace Twixter.Service.Services.Concretes;

public sealed class CommentService(IMapper mapper, ICommentRepository commentRepository) : ICommentService
{
    public async Task<CommentResponseDto> GetByIdAsync(Guid id)
    {
        var comment = await commentRepository.FindByIdAsync(id);
        var response = mapper.Map<CommentResponseDto>(comment);
        return response;
    }

    public async Task<List<CommentResponseDto>> GetAllAsync()
    {
        var comments =
            await commentRepository.GetAllAsync(x => x.IsDeleted == false, enableTracking: false, include: false);
        var responses = mapper.Map<List<CommentResponseDto>>(comments);
        return responses;
    }

    public async Task AddAsync(CommentAddRequestDto commentAddRequestDto)
    {
        var comment = mapper.Map<Comment>(commentAddRequestDto);
        await commentRepository.AddAsync(comment);
    }

    public async Task DeleteAsync(Guid id)
    {
        var comment = await commentRepository.FindByIdAsync(id);
        await commentRepository.DeleteAsync(comment ?? throw new Exception("This comment not found!"));
    }

    public async Task<List<CommentResponseDto>> GetAllByPostId(Guid postId)
    {
        var comments = await commentRepository.GetAllAsync(x => x.PostId == postId);
        var responses = mapper.Map<List<CommentResponseDto>>(comments);
        return responses;
    }
}