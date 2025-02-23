using Twixter.Models.Dtos.Posts;

namespace Twixter.Service.Services.Abstracts;

public interface IPostService
{
    Task<PostResponseDto> GetByIdAsync(Guid id);
    Task<List<PostResponseDto>> GetAllAsync();
    Task AddAsync(PostAddRequestDto postAddRequestDto);
    Task DeleteAsync(Guid id);
    Task<List<PostResponseDto>> GetAllByUserId(Guid userId);
}