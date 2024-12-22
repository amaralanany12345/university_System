using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSystem.Models;

namespace SchoolSystem.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("teachers").HasKey(a => a.id);
            builder.Property(a => a.id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.userName).IsRequired().HasMaxLength(255);
            builder.Property(a => a.email).IsRequired().HasMaxLength(255);
            builder.HasIndex(a => a.email).IsUnique(true);
            builder.Property(a => a.password).IsRequired().HasMaxLength(255);
            builder.HasMany(a => a.groupTeachers).WithOne(a => a.teacher).HasForeignKey(a => a.teacherId);
        }
    }
}
