using Twixter.Models.Entities;

namespace Twixter.Repository.Repositories.Abstracts;

public interface IApplicationUserRepository
{
    Task<ApplicationUser?> FindByIdAsync(Guid id);
    Task<ApplicationUser> AddAsync(ApplicationUser user);
    Task<ApplicationUser> UpdateAsync(ApplicationUser user);
    Task<ApplicationUser> DeleteAsync(ApplicationUser user);
}