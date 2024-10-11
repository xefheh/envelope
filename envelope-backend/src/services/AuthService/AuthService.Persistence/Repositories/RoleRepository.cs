using AuthService.Application.Repositories;
using AuthService.Domain.Entities;
using AuthService.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistance.Repositories;

public class RoleRepository(AuthContext context) : IRoleRepository
{
    private readonly AuthContext _context = context;

    public async Task<Role> GetDefaultRole()
    {
        return await _context.Roles.FirstAsync(role => role.Name == "Student");
    }
}