using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSystem.Models;

namespace SchoolSystem.Configurations
{
    public class CourseWorkConfiguration : IEntityTypeConfiguration<CourseWork>
    {
        public void Configure(EntityTypeBuilder<CourseWork> builder)
        {
            builder.ToTable("CourseWorks").HasKey(a=>new {a.studentId,a.courseId });
            builder.Property(a => a.studentScore).IsRequired().HasDefaultValue(0);
            builder.Property(a => a.studentCourseRate).IsRequired();
            builder.HasOne(a => a.course).WithMany(a => a.courseWorks).HasForeignKey(a => a.courseId);
        }
    }
}
