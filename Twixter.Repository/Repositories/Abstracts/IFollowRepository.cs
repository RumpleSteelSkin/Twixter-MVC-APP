using CorePackage.Repositories;
using Twixter.Models.Entities;

namespace Twixter.Repository.Repositories.Abstracts;

public interface IFollowRepository : IRepository<Follow, Guid>;