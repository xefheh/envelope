using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskService.Domain.Projections;

namespace TaskService.Persistence.Configurations.ProjectionConfigurations;

public class TaskProjectionConfiguration : IEntityTypeConfiguration<TaskProjection>
{
    public void Configure(EntityTypeBuilder<TaskProjection> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Id).IsUnique();
    }
}