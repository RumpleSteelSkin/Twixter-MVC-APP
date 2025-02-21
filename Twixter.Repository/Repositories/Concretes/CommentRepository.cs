using CorePackage.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Twixter.Models.Entities;
using Twixter.Repository.Context;
using Twixter.Repository.Repositories.Abstracts;

namespace Twixter.Repository.Repositories.Concretes;

public sealed class CommentRepository(
    ApplicationDbContext context,
    ILogger<EfRepositoryBase<Comment, Guid, ApplicationDbContext>> logger,
    IMemoryCache? cache = null,
    bool useCaching = false,
    bool useSoftDelete = false)
    : EfRepositoryBase<Comment, Guid, ApplicationDbContext>(context, logger, cache, useCaching, useSoftDelete), 
        ICommentRepository { }