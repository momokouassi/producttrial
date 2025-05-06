using ProductTrial.Data.Dtos;

namespace ProductTrial.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateAsync(UserCreationDto user);

        Task<string> LogInAsync(UserConnectionDto user);
    }
}