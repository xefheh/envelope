using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskService.Domain.Events;
using TaskService.Domain.Events.Base;
using TaskService.Domain.Interfaces;

namespace TaskService.Persistence.Configurations.EventConfigurations;

public class TaskEventRepositoryConfiguration : IEntityTypeConfiguration<BaseTaskEvent>
{
    public void Configure(EntityTypeBuilder<BaseTaskEvent> builder)
    {
        builder.HasKey(e => new { e.Id, e.VersionId });
        builder.HasIndex(e => new { e.Id, e.VersionId}).IsUnique();
        builder.HasDiscriminator()
            .HasValue<BaseTaskCreated>("TaskCreated")
            .HasValue<BaseTaskUpdated>("TaskUpdated")
            .HasValue<BaseTaskRemoved>("TaskRemoved")
            .HasValue<BaseTaskSentToCheck>("TaskSentToCheck")
            .HasValue<BaseTaskSentToGlobal>("TaskSentToGlobal")
            .HasValue<BaseTaskRefused>("TaskRefused")
            .IsComplete();
    }
}