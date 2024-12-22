using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSystem.Models;

namespace SchoolSystem.Configurations
{
    public class FinalResultConfiguration : IEntityTypeConfiguration<FinalResult>
    {
        public void Configure(EntityTypeBuilder<FinalResult> builder)
        {
            builder.ToTable("finalResults").HasKey(a => a.id);
            builder.Property(a => a.id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.totalScore).IsRequired();
            builder.Property(a => a.finalScore).IsRequired();
            builder.Property(a => a.finalRate).IsRequired();
            builder.HasOne(a => a.student).WithMany(a => a.finalResults).HasForeignKey(a => a.studentId);
        }
    }
}
