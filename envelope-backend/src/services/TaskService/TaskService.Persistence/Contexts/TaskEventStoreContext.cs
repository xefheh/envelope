using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TaskService.Domain.Events;
using TaskService.Domain.Interfaces;

namespace TaskService.Persistence.Contexts;

public class TaskEventStoreContext : DbContext
{
    public TaskEventStoreContext(DbContextOptions<TaskEventStoreContext> options) : base(options) { }
    
    public DbSet<ITaskEvent> TaskEvents { get; set; }
    public DbSet<TaskCreated> TaskCreatedEvents { get; set; }
    public DbSet<TaskRefused> TaskRefusedEvents { get; set; }
    public DbSet<TaskRemoved> TaskRemovedEvents { get; set; }
    public DbSet<TaskSentToCheck> TaskSentToCheckEvents { get; set; }
    public DbSet<TaskSentToGlobal> TaskSentToGlobalEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}