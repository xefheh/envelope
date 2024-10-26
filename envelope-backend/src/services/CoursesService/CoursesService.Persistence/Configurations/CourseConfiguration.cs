using CoursesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoursesService.Persistence.Configurations;

public class CourseConfiguration: IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(course => course.Id);
        builder.HasIndex(course => course.Id).IsUnique();
        builder.OwnsMany(course => course.Blocks)
            .WithOwner(block => block.Course);
    }
}