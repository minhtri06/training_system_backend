using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Class> Classes { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<CourseCertificate> CourseCertificates { get; set; } =
            null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<DepartmentLearningPath> DepartmentLearningPaths { get; set; } =
            null!;
        public DbSet<LearningPath> LearningPaths { get; set; } = null!;
        public DbSet<LearningPathCertificate> LearningPathCertificates { get; set; } =
            null!;
        public DbSet<LearningPathCourse> LearningPathCourses { get; set; } =
            null!;
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
                    .WithMany(c => c.CourseCertificates)
                    .HasForeignKey(cc => cc.CourseId);

                e.HasOne(cc => cc.Trainee)
                    .WithMany(t => t.CourseCertificates)
                    .HasForeignKey(cc => cc.TraineeId);
            });

            modelBuilder.Entity<Department>(e =>
            {
                e.HasKey("Id");
                e.Property(d => d.Name).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<DepartmentLearningPath>(e =>
            {
                e.HasKey(dl => new { dl.DepartmentId, dl.LearningPathId });

                e.HasOne(dl => dl.Department)
                    .WithMany(d => d.DepartmentLearningPaths)
                    .HasForeignKey(dl => dl.DepartmentId);

                e.HasOne(dl => dl.LearningPath)
                    .WithMany(l => l.DepartmentLearningPaths)
                    .HasForeignKey(dl => dl.LearningPathId);
            });

            modelBuilder.Entity<LearningPath>(e =>
            {
                e.HasKey("Id");
                e.Property(l => l.Name).IsRequired().HasMaxLength(50);

                e.HasOne(l => l.ForRole)
                    .WithMany(r => r.LearningPaths)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<LearningPathCertificate>(e =>
            {
                e.HasKey(lc => new { lc.TraineeId, lc.LearningPathId });

                e.HasOne(lc => lc.Trainee)
                    .WithMany(t => t.LearningPathCertificates)
                    .HasForeignKey(lc => lc.TraineeId);

                e.HasOne(lc => lc.LearningPath)
                    .WithMany(l => l.LearningPathCertificates)
                    .HasForeignKey(lc => lc.LearningPathId);
            });

            modelBuilder.Entity<LearningPathCourse>(e =>
            {
                e.HasKey(lc => new { lc.LearningPathId, lc.CourseId });

                e.HasOne(lc => lc.LearningPath)
                    .WithMany(l => l.LearningPathCourses)
                    .HasForeignKey(lc => lc.LearningPathId);

                e.HasOne(lc => lc.Course)
                    .WithMany(c => c.LearningPathCourses)
                    .HasForeignKey(lc => lc.CourseId);
            });

            modelBuilder.Entity<Role>(e =>
            {
                e.HasKey("Id");
                e.Property(r => r.Name).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Trainee>(e =>
            {
                e.HasKey("Id");
                e.Property(t => t.FirstName).HasMaxLength(50);
                e.Property(t => t.LastName).HasMaxLength(50);
                e.Property(t => t.SystemRole).IsRequired().HasMaxLength(50);
                e.Property(t => t.passwordHash).IsRequired().HasMaxLength(250);
                e.Property(t => t.PasswordSalt).IsRequired().HasMaxLength(250);

                e.HasOne(t => t.Role)
                    .WithMany(r => r.Trainees)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<TraineeClass>(e =>
            {
                e.HasKey(tc => new { tc.TraineeId, tc.ClassId });
                e.Property(tc => tc.status)
                    .HasDefaultValue(TraineeLearningState.InProgress);

                e.HasOne(tc => tc.Trainee)
                    .WithMany(t => t.TraineeClasses)
                    .HasForeignKey(tc => tc.TraineeId);

                e.HasOne(tc => tc.Class)
                    .WithMany(c => c.TraineeClasses)
                    .HasForeignKey(tc => tc.ClassId);
            });

            modelBuilder.Entity<Trainer>(e =>
            {
                e.HasKey("Id");
                e.Property(t => t.SystemRole).IsRequired().HasMaxLength(50);
                e.Property(t => t.passwordHash).IsRequired().HasMaxLength(250);
                e.Property(t => t.PasswordSalt).IsRequired().HasMaxLength(250);
            });
        }
    }
}
