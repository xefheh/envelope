using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskService.Domain.Projections;

namespace TaskService.Persistence.Configurations.ProjectionConfigurations;

public class GlobalProjectionConfiguration : IEntityTypeConfiguration<GlobalTaskProjection>
{
    public void Configure(EntityTypeBuilder<GlobalTaskProjection> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Id).IsUnique();
    }
}