using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSystem.Models;

namespace SchoolSystem.Configurations
{
    public class StudentsConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("students").HasKey(a => a.id);
            builder.Property(a => a.id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.userName).IsRequired().HasMaxLength(255);
            builder.Property(a => a.email).IsRequired().HasMaxLength(255);
            builder.HasIndex(a => a.email).IsUnique(true);
            builder.Property(a => a.password).IsRequired().HasMaxLength(255);
            builder.HasOne(a => a.group).WithMany(a => a.students).HasForeignKey(a => a.groupId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(a => a.studentCourses).WithOne(a => a.student).HasForeignKey(a => a.studentId);
        }
    }
}
