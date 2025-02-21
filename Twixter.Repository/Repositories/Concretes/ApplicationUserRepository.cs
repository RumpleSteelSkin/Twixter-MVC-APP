using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Twixter.Models.Entities;
using Twixter.Repository.Context;
using Twixter.Repository.Repositories.Abstracts;

namespace Twixter.Repository.Repositories.Concretes;

public sealed class ApplicationUserRepository(
    ApplicationDbContext context,
    ILogger<ApplicationUserRepository> logger,
    IMemoryCache? cache = null)
    : IApplicationUserRepository
{
    public async Task<ApplicationUser?> FindByIdAsync(Guid id)
    {
        if (cache is not null && cache.TryGetValue(id, out ApplicationUser? cachedUser))
        {
            return cachedUser;
        }

        var user = await context.Users.FindAsync(id);

        if (user is not null && cache is not null)
        {
            cache.Set(id, user, TimeSpan.FromMinutes(10));
        }

        return user;
    }

    public async Task<ApplicationUser> AddAsync(ApplicationUser user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        logger.LogInformation("New user added: {UserId}", user.Id);
        return user;
    }

    public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
        logger.LogInformation("User updated: {UserId}", user.Id);
        return user;
    }

    public async Task<ApplicationUser> DeleteAsync(ApplicationUser user)
    {
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        logger.LogInformation("User deleted: {UserId}", user.Id);
        return user;
    }
}