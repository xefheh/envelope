using AuthService.Domain.Entities;

namespace AuthService.Application.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);

    Task<User?> GetUserByNickname(string email);

    Task Create(User user);
}