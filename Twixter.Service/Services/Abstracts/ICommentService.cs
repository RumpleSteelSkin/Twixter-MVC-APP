using Twixter.Models.Dtos.Comments;
using Twixter.Models.Entities;

namespace Twixter.Service.Services.Abstracts;

public interface ICommentService
{
    Task<CommentResponseDto> GetByIdAsync(Guid id);
    Task<List<CommentResponseDto>> GetAllAsync();
    Task AddAsync(CommentAddRequestDto commentAddRequestDto);
    Task DeleteAsync(Guid id);
    Task<List<CommentResponseDto>> GetAllByPostId(Guid postId);
}