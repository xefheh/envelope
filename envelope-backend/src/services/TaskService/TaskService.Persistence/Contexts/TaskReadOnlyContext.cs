using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TaskService.Domain.Projections;

namespace TaskService.Persistence.Contexts;

public class TaskReadOnlyContext : DbContext
{
    public TaskReadOnlyContext(DbContextOptions<TaskReadOnlyContext> options) : base(options) { }

    public DbSet<TaskProjection> TaskProjections { get; set; } = null!;
    public DbSet<GlobalTaskProjection> GlobalTaskProjections { get; set; } = null!;
    public DbSet<SentToCheckTaskProjection> SentToCheckTaskProjections { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}