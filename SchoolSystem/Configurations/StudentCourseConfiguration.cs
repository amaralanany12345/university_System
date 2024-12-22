using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSystem.Models;

namespace SchoolSystem.Configurations
{
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.ToTable("studentCourses").HasKey(a => new { a.studentId, a.courseId});
            builder.HasOne(a => a.course).WithMany(a => a.studentCourses).HasForeignKey(a => a.courseId);
            builder.HasOne(a => a.student).WithMany(a => a.studentCourses).HasForeignKey(a => a.studentId);
        }

    }
}
