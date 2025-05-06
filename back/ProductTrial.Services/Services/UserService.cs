using Microsoft.EntityFrameworkCore;
using ProductTrial.Data.Dtos;
using ProductTrial.Data.Entities;
using ProductTrial.Services.Interfaces;
using ProductTrial.Services.Middlewares.ExceptionHandler.Exceptions;
using System.Security.Cryptography;
using System.Text.Json;

namespace ProductTrial.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<ProductTrialDbContext> _contextFactory;
        private readonly IPasswordEncriptionService _passwordEncriptionService;
        private readonly ITokenService _tokenService;

        public UserService(IDbContextFactory<ProductTrialDbContext> contextFactory, IPasswordEncriptionService passwordEncriptionService, ITokenService tokenService)
        {
            _contextFactory = contextFactory;
            _passwordEncriptionService = passwordEncriptionService;
            _tokenService = tokenService;
        }

        public async Task<UserDto> CreateAsync(UserCreationDto dto)
        {
            try
            {
                User newUser = new User(dto.Username, dto.Firstname, dto.Email, _passwordEncriptionService.Encrypt(dto.Password, HashAlgorithmName.SHA256));
                await using (ProductTrialDbContext context = _contextFactory.CreateDbContext())
                {
                    User? existingUser = await context.Users.FirstOrDefaultAsync(f => f.Username == dto.Username);
                    if (existingUser != null)
                    {
                        throw new AlreadyExistsException($"The given user already exists.");
                    }

                    await context.Users.AddAsync(newUser);
                    int changeCount = await context.SaveChangesAsync();
                    if (changeCount == 0)
                    {
                        throw new DbUpdateException($"The given product can not be saved : {JsonSerializer.Serialize(newUser)}");
                    }
                }
                return new UserDto(newUser.Username, newUser.Firstname, newUser.Email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> LogInAsync(UserConnectionDto user)
        {
            try
            {
                await using (ProductTrialDbContext context = _contextFactory.CreateDbContext())
                {
                    User? existingUser = await context.Users.FirstOrDefaultAsync(f => f.Email == user.Email);
                    if (existingUser == null)
                    {
                        throw new UnauthorizedAccessException($"The given user is unknown.");
                    }

                    bool isMatching = _passwordEncriptionService.VerifyHashedPassword(existingUser.Password, user.Password);
                    if (!isMatching)
                    {
                        throw new ForbiddenAccessException($"The given user is unauthorized.");
                    }

                    return _tokenService.CreateToken(existingUser.Username, existingUser.Firstname, existingUser.Email);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}