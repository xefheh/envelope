using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace AuthService.Persistance.Data;

public class AuthContext(DbContextOptions<AuthContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>().HasData(
            new Role() { Name = "Student" },
            new Role() { Name = "Teacher" },
            new Role() { Name = "Moderator" });
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
}