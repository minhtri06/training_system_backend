using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Class> Classes { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<CourseCertificate> CourseCertificates { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<DepartmentLearningPath> DepartmentLearningPaths { get; set; } = null!;
        public DbSet<LearningPath> LearningPaths { get; set; } = null!;
        public DbSet<LearningPathCertificate> LearningPathCertificates { get; set; } = null!;
        public DbSet<LearningPathCourse> LearningPathCourses { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Trainee> Trainees { get; set; } = null!;
        public DbSet<TraineeClass> TraineeClasses { get; set; } = null!;
        public DbSet<Trainer> Trainers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(e =>
            {
                e.HasKey("Id");
                e.Property(c => c.Name).IsRequired().HasMaxLength(50);
                e.Property(c => c.CourseId).IsRequired();
            });

            modelBuilder.Entity<Course>(e =>
            {
                e.HasKey("Id");
                e.Property(c => c.Name).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<CourseCertificate>(e =>
            {
                e.HasKey(cc => new { cc.CourseId, cc.TraineeId });

                e.HasOne(cc => cc.Course)
                    .WithMany(cc => cc.CourseCertificates)
                    .HasForeignKey(cc => cc.CourseId);

                e.HasOne(cc => cc.Trainee)
                    .WithMany(cc => cc.CourseCertificates)
                    .HasForeignKey(cc => cc.TraineeId);
            });

            modelBuilder.Entity<Department>(e =>
            {
                e.HasKey("Id");
                e.Property(d => d.Name).IsRequired().HasMaxLength(50);
            });
        }
    }
}
