using AuthService.Domain.Entities;

namespace AuthService.Application.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);

    Task<User?> GetUserByNickname(string email);

    Task<User?> GetUserById(Guid id);

    Task Create(User user);
}