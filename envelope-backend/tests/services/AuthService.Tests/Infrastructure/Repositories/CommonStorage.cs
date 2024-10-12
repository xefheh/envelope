using AuthService.Domain.Entities;

namespace AuthService.Tests.Infrastructure.Repositories;

public class CommonStorage
{
    public List<User> Users { get; set; } = [];
}