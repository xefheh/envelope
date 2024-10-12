using AuthService.Application.Repositories;
using AuthService.Domain.Entities;

namespace AuthService.Tests.Infrastructure.Repositories;

public class MockCommonAuthRepository (CommonStorage commonStorage) : IUserRepository, IRoleRepository
{
    private readonly CommonStorage _commonStorage = commonStorage;

    public async Task Create(User user)
    {
        await Task.Run(() => _commonStorage.Users.Add(user));
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await Task.Run(() => _commonStorage.Users.FirstOrDefault(user => user.Email == email));
    }

    public async Task<User?> GetUserByNickname(string nickname)
    {
        return await Task.Run(() => _commonStorage.Users.FirstOrDefault(user => user.Nickname == nickname));
    }

    public async Task<Role> GetDefaultRole()
    {
        return await Task.Run(() => _commonStorage.Roles.First(role => role.Name == "Student"));
    }
}