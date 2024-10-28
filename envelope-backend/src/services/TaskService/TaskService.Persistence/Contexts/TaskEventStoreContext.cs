using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TaskService.Domain.Events;
using TaskService.Domain.Events.Base;
using TaskService.Domain.Interfaces;

namespace TaskService.Persistence.Contexts;

public class TaskEventStoreContext : DbContext
{
    public TaskEventStoreContext(DbContextOptions<TaskEventStoreContext> options) : base(options) { }
    
    public DbSet<BaseTaskEvent> TaskEvents { get; set; }
    public DbSet<BaseTaskCreated> TaskCreatedEvents { get; set; }
    public DbSet<BaseTaskRefused> TaskRefusedEvents { get; set; }
    public DbSet<BaseTaskRemoved> TaskRemovedEvents { get; set; }
    public DbSet<BaseTaskSentToCheck> TaskSentToCheckEvents { get; set; }
    public DbSet<BaseTaskSentToGlobal> TaskSentToGlobalEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}