using Microsoft.EntityFrameworkCore;
using SchoolSystem.Models;

namespace SchoolSystem.General
{
    public class AppDbContext:DbContext
    {
        public DbSet<Student> students { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Group> groups { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<GroupTeacher> groupTeachers { get; set; }
        public DbSet<StudentCourse> studentCourses { get; set; }
        public DbSet<Attendance> attendances { get; set;  }
        public DbSet<CourseWork> courseWorks { get; set; }
        public DbSet<FinalResult> finalResults { get; set; }
        public DbSet<FailedCourse> failedCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-NNDJ4G3D\SQLEXPRESS; Database=UniversitySystem; Integrated Security=SSPI; TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
