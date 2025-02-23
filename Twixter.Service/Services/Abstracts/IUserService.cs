using Twixter.Models.Dtos.Users;

namespace Twixter.Service.Services.Abstracts;

public interface IUserService
{
    Task<UserResponseDto> CreateUserAsync(RegisterRequestDto register);
    Task<UserResponseDto> LoginAsync(LoginRequestDto login);
    Task<UserResponseDto> GetByEmailAsync(string email);
}