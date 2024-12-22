using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSystem.Models;

namespace SchoolSystem.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("groups").HasKey(a => a.id);
            builder.Property(a => a.id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.totalScoreOfTheYear).IsRequired().HasDefaultValue(0);
            builder.HasMany(a => a.groupTeachers).WithOne(a => a.group).HasForeignKey(a => a.groupId);
            builder.HasMany(a => a.students).WithOne(a => a.group).HasForeignKey(a => a.groupId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
