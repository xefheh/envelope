using AuthService.Application.Repositories;
using AuthService.Domain.Entities;

namespace AuthService.Tests.Infrastructure.Repositories;

/// <summary>
/// Класс для имитации репозиториев для тестов
/// </summary>
public class MockCommonAuthRepository : IUserRepository
{
    private readonly CommonStorage _commonStorage;

    public MockCommonAuthRepository(CommonStorage commonStorage)
    {
        _commonStorage = commonStorage;
    }

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

    public async Task<User?> GetUserById(Guid id)
    {
        return await Task.Run(() => _commonStorage.Users.FirstOrDefault(user => user.Id == id));
    }
}