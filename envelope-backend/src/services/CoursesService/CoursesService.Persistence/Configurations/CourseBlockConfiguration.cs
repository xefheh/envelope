using CoursesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoursesService.Persistence.Configurations;

public class CourseBlockConfiguration : IEntityTypeConfiguration<CourseBlock>
{
    public void Configure(EntityTypeBuilder<CourseBlock> builder)
    {
        builder.HasKey(b => b.Id);
        builder.HasIndex(b => b.Id).IsUnique();
        builder.OwnsMany(b => b.Tasks);
    }
}