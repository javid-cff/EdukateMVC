using EdukateMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdukateMVC.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Rating).IsRequired();
            builder.Property(x => x.Review).IsRequired();
            builder.Property(x => x.ImagePath).IsRequired();

            builder.HasOne(x => x.Teacher).WithMany(x => x.Courses).HasForeignKey(x => x.TeacherId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
