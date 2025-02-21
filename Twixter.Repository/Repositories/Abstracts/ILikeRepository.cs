using CorePackage.Repositories;
using Twixter.Models.Entities;

namespace Twixter.Repository.Repositories.Abstracts;

public interface ILikeRepository : IRepository<Like, Guid>;