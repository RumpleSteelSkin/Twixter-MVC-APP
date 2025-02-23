using AutoMapper;
using Twixter.Models.Dtos.Posts;
using Twixter.Models.Entities;
using Twixter.Repository.Repositories.Abstracts;
using Twixter.Service.Services.Abstracts;

namespace Twixter.Service.Services.Concretes;

public class PostService(IMapper mapper, IPostRepository postRepository):IPostService
{
    public async Task<PostResponseDto> GetByIdAsync(Guid id)
    {
        var post = await postRepository.FindByIdAsync(id);
        var response = mapper.Map<PostResponseDto>(post);
        return response;
    }

    public async Task<List<PostResponseDto>> GetAllAsync()
    {
        var posts = await postRepository.GetAllAsync();
        var responses = mapper.Map<List<PostResponseDto>>(posts);
        return responses;
    }

    public async Task AddAsync(PostAddRequestDto postAddRequestDto)
    {
        var post = mapper.Map<Post>(postAddRequestDto);
        await postRepository.AddAsync(post);
    }

    public async Task DeleteAsync(Guid id)
    {
        var post = await postRepository.FindByIdAsync(id);
        await postRepository.DeleteAsync(post ?? throw new Exception("This post not found"));
    }

    public async Task<List<PostResponseDto>> GetAllByUserId(Guid userId)
    {
        var posts = await postRepository.GetAllAsync(x => x.UserId == userId);
        var responses = mapper.Map<List<PostResponseDto>>(posts);
        return responses;
    }
}