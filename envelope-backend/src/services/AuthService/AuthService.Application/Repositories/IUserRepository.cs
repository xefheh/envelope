using AuthService.Domain.Entities;

namespace AuthService.Application.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);

    Task Create(User user);
}
