using AuthService.Domain.Entities;

namespace AuthService.Tests.Infrastructure.Repositories;

/// <summary>
/// Класс для хранение данных пользователя для тестов 
/// </summary>
public class CommonStorage
{
    public List<User> Users { get; set; } = [];
}