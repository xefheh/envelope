using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskService.Domain.Events;
using TaskService.Domain.Interfaces;

namespace TaskService.Persistence.Configurations.EventConfigurations;

public class TaskEventRepositoryConfiguration : IEntityTypeConfiguration<ITaskEvent>
{
    public void Configure(EntityTypeBuilder<ITaskEvent> builder)
    {
        builder.HasKey(e => new { e.Id, e.VersionId });
        builder.HasIndex(e => new { e.Id, e.VersionId}).IsUnique();
        builder.HasDiscriminator()
            .HasValue<TaskCreated>("TaskCreated")
            .HasValue<TaskUpdated>("TaskUpdated")
            .HasValue<TaskRemoved>("TaskRemoved")
            .HasValue<TaskSentToCheck>("TaskSentToCheck")
            .HasValue<TaskSentToGlobal>("TaskSentToGlobal")
            .HasValue<TaskRefused>("TaskRefused")
            .IsComplete();
    }
}