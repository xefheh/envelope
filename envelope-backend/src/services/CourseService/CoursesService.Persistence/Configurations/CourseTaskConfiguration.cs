using CoursesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoursesService.Persistence.Configurations;

public class CourseTaskConfiguration : IEntityTypeConfiguration<CourseTask>
{
    public void Configure(EntityTypeBuilder<CourseTask> builder)
    {
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => t.Id).IsUnique();
    }
}