using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Twixter.Models.Entities;
using Twixter.Repository.Context;
using Twixter.Repository.Repositories.Abstracts;
using Twixter.Repository.Repositories.Concretes;
using Twixter.Service.Mappers;
using Twixter.Service.Services.Abstracts;
using Twixter.Service.Services.Concretes;

namespace Twixter.WebMVC.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database Connection
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Identity Configuration
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        //Repository IoC
        services.AddScoped<ICommentRepository,CommentRepository>();
        services.AddScoped<IFollowRepository,FollowRepository>();
        services.AddScoped<ILikeRepository,LikeRepository>();
        services.AddScoped<IMediaRepository,MediaRepository>();
        services.AddScoped<IPostRepository,PostRepository>();
        
        //Services IoC
        services.AddScoped<ICommentService,CommentService>();
        services.AddScoped<IPostService,PostService>();
        services.AddScoped<IUserService, UserService>();
        
        //AutoMapper IoC
        services.AddAutoMapper(typeof(MappingProfile));
        
        return services;
    }
}