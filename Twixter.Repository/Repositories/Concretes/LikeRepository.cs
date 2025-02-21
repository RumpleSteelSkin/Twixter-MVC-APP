﻿using CorePackage.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Twixter.Models.Entities;
using Twixter.Repository.Context;
using Twixter.Repository.Repositories.Abstracts;

namespace Twixter.Repository.Repositories.Concretes;

public sealed class LikeRepository(
    ApplicationDbContext context,
    ILogger<EfRepositoryBase<Like, Guid, ApplicationDbContext>> logger,
    IMemoryCache? cache = null,
    bool useCaching = false,
    bool useSoftDelete = false)
    : EfRepositoryBase<Like, Guid, ApplicationDbContext>(context, logger, cache, useCaching, useSoftDelete), 
        ILikeRepository { }