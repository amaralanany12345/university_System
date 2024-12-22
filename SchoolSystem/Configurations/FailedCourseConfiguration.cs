using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSystem.Models;

namespace SchoolSystem.Configurations
{
    public class FailedCourseConfiguration : IEntityTypeConfiguration<FailedCourse>
    {
        public void Configure(EntityTypeBuilder<FailedCourse> builder)
        {
            builder.ToTable("failedCourses").HasKey(a => new { a.studentId,a.courseId});
            builder.HasOne(a => a.student).WithMany(a => a.failedCourses).HasForeignKey(a => a.studentId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.course).WithMany(a => a.failedCourses).HasForeignKey(a => a.courseId);
            builder.HasOne(a => a.finalResult).WithMany(a => a.failedCourses).HasForeignKey(a => a.finalResultId);
        }
    }
}
