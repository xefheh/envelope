using AuthService.Application.Repositories;
using AuthService.Domain.Entities;
using AuthService.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistance.Repositories;

public class UserRepository : IUserRepository 
{
    private readonly AuthContext _context;

    public UserRepository(AuthContext context)
    {
        _context = context;
    }

    public async Task Create(User user)
    {
        _context.Attach(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<User?> GetUserByNickname(string nickname)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Nickname == nickname);
    }
}