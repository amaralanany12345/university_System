using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSystem.Models;

namespace SchoolSystem.Configurations
{
    public class GroupTeacherConfiguration : IEntityTypeConfiguration<GroupTeacher>
    {
        public void Configure(EntityTypeBuilder<GroupTeacher> builder)
        {
            builder.ToTable("groupTeachers").HasKey(a => new { a.teacherId, a.groupId});
            builder.HasOne(a => a.teacher).WithMany(t => t.groupTeachers).HasForeignKey(a => a.teacherId);
            builder.HasOne(a => a.group).WithMany(a => a.groupTeachers).HasForeignKey(a => a.groupId);
        }

    }
}
