using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskService.Domain.Projections;

namespace TaskService.Persistence.Configurations.ProjectionConfigurations;

public class SentToCheckTaskProjectionConfiguration : IEntityTypeConfiguration<SentToCheckTaskProjection>
{
    public void Configure(EntityTypeBuilder<SentToCheckTaskProjection> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Id).IsUnique();
    }
}