using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Twixter.Models.Dtos.Users;
using Twixter.Models.Entities;
using Twixter.Service.Exceptions;
using Twixter.Service.Services.Abstracts;

namespace Twixter.Service.Services.Concretes;

public sealed class UserService(UserManager<ApplicationUser> userManager, IMapper mapper) : IUserService
{
    public async Task<UserResponseDto> CreateUserAsync(RegisterRequestDto register)
    {
        ApplicationUser user = mapper.Map<ApplicationUser>(register);
        
        var result = await userManager.CreateAsync(user, register.Password);

        if (!result.Succeeded)
        {
            string errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new UserCreationException($"Kullanıcı oluşturulamadı: {errors}");
        }
        

        UserResponseDto dto = mapper.Map<UserResponseDto>(user);
        return dto;
    }

    public async Task<UserResponseDto> LoginAsync(LoginRequestDto login)
    {
        var user = await userManager.FindByEmailAsync(login.Email);
        if (user is null)
        {
            throw new UserNotFoundException("Kullanıcı bulunamadı.");
        }

        if (!await userManager.CheckPasswordAsync(user, login.Password))
        {
            throw new InvalidPasswordException("Geçersiz parola.");
        }
        
        if (!user.EmailConfirmed)
        {
            throw new EmailNotConfirmedException("E-posta adresiniz doğrulanmamış.");
        }

        if (await userManager.IsLockedOutAsync(user))
        {
            throw new UserLockedOutException("Hesabınız geçici olarak kilitlenmiş.");
        }
        
        UserResponseDto dto = mapper.Map<UserResponseDto>(user);
        return dto;
    }

    public async Task<UserResponseDto> GetByEmailAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            throw new UserNotFoundException("Kullanıcı bulunamadı.");
        }
        
        UserResponseDto dto = mapper.Map<UserResponseDto>(user);
        return dto;
    }
}
