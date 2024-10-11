using AuthService.Domain.Entities;

namespace AuthService.Application.Repositories;

public interface IRoleRepository
{
    Task<Role> GetDefaultRole();
}