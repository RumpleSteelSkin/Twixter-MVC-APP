using CorePackage.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Twixter.Models.Dtos.Posts;
using Twixter.Models.Entities;
using Twixter.Repository.Context;
using Twixter.Repository.Repositories.Abstracts;

namespace Twixter.Repository.Repositories.Concretes;

public sealed class PostRepository(
    ApplicationDbContext context,
    ILogger<EfRepositoryBase<Post, Guid, ApplicationDbContext>> logger,
    IMemoryCache? cache = null,
    bool useCaching = false,
    bool useSoftDelete = false)
    : EfRepositoryBase<Post, Guid, ApplicationDbContext>(context, logger, cache, useCaching, useSoftDelete),
        IPostRepository
{
    // public List<PostResponseDto> ResponseDtos
}