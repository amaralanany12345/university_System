using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSystem.Models;

namespace SchoolSystem.Configurations
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("attendances").HasKey(a =>new {a.studentId,a.courseId});
            builder.Property(a => a.numOfAttendances).IsRequired().HasDefaultValue(0);
            builder.Property(a => a.numOfAbsences).IsRequired().HasDefaultValue(0);
            builder.HasOne(a => a.student).WithMany(a => a.attendances).HasForeignKey(a => a.studentId);
            builder.HasOne(a => a.course).WithMany(a => a.attendances).HasForeignKey(a => a.courseId);
        }
    }
}
