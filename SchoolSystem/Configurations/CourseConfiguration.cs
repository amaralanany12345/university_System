using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSystem.Models;

namespace SchoolSystem.Configurations
{
    public class CourseConfiguration:IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("courses").HasKey(a => a.id);
            builder.Property(a => a.id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.courseName).IsRequired().HasMaxLength(255);
            builder.Property(a => a.description).IsRequired().HasMaxLength(255);
            builder.Property(a => a.academicYear).IsRequired().HasMaxLength(255);
            builder.Property(a => a.score).IsRequired();
            builder.HasOne(a => a.teacher).WithMany(a => a.courses).HasForeignKey(a => a.teacherId);
            builder.HasOne(a => a.group).WithMany(a => a.groupCourses).HasForeignKey(a => a.groupId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(a => a.studentCourses).WithOne(a => a.course).HasForeignKey(a => a.courseId);
        }
    }
}
