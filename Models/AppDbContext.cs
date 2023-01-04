using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<AdminUser> AdminUsers { get; set; } = null!;
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
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<Trainee> Trainees { get; set; } = null!;
        public DbSet<TraineeClass> TraineeClasses { get; set; } = null!;
        public DbSet<Trainer> Trainers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminUser>(e =>
            {
                e.HasKey("Id");
                e.Property(au => au.Username).IsRequired();
                e.Property(au => au.PasswordHash).IsRequired();
                e.Property(au => au.PasswordSalt).IsRequired();

                e.HasIndex(au => au.Username).IsUnique();

                e.HasOne(au => au.RefreshToken)
                    .WithOne(t => t.AdminUser)
                    .HasForeignKey<AdminUser>(au => au.RefreshTokenId)
                    .OnDelete(DeleteBehavior.SetNull);
                ;
            });

            modelBuilder.Entity<Class>(e =>
            {
                e.HasKey("Id");
                e.Property(c => c.Name).IsRequired().HasMaxLength(50);

                e.HasOne(c => c.Course)
                    .WithMany(course => course.Classes)
                    .HasForeignKey(c => c.CourseId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Course>(e =>
            {
                e.HasKey("Id");
                e.Property(c => c.Name).IsRequired().HasMaxLength(50);

                e.HasOne(c => c.Trainer)
                    .WithMany(t => t.Courses)
                    .HasForeignKey(c => c.TrainerId)
                    .OnDelete(DeleteBehavior.SetNull);
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
                    .HasForeignKey(l => l.ForRoleId)
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

            modelBuilder.Entity<RefreshToken>(e =>
            {
                e.HasKey("Id");
                e.Property(t => t.Token).IsRequired();
                e.Property(t => t.ExpiryTime).IsRequired();
                e.Property(t => t.CreatedTime).IsRequired();

                e.HasOne(t => t.AdminUser).WithOne(au => au.RefreshToken);

                e.HasOne(t => t.Trainee).WithOne(t => t.RefreshToken);

                e.HasOne(t => t.Trainer).WithOne(t => t.RefreshToken);
            });

            modelBuilder.Entity<Trainee>(e =>
            {
                e.HasKey("Id");
                e.Property(t => t.FirstName).HasMaxLength(50);
                e.Property(t => t.LastName).HasMaxLength(50);
                e.Property(t => t.SystemRole).IsRequired().HasMaxLength(50);
                e.Property(t => t.Username).IsRequired().HasMaxLength(250);
                e.Property(t => t.PasswordHash).IsRequired().HasMaxLength(250);
                e.Property(t => t.PasswordSalt).IsRequired().HasMaxLength(250);
                e.HasIndex(t => t.Username).IsUnique();

                e.HasOne(t => t.Role)
                    .WithMany(r => r.Trainees)
                    .HasForeignKey(t => t.RoleId)
                    .OnDelete(DeleteBehavior.SetNull);

                e.HasOne(t => t.Department)
                    .WithMany(d => d.Trainees)
                    .HasForeignKey(t => t.DepartmentId)
                    .OnDelete(DeleteBehavior.SetNull);

                e.HasOne(t => t.RefreshToken)
                    .WithOne(t => t.Trainee)
                    .HasForeignKey<Trainee>(t => t.TokenId)
                    .OnDelete(DeleteBehavior.SetNull);
                ;
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
                e.Property(t => t.Username).IsRequired().HasMaxLength(250);
                e.Property(t => t.passwordHash).IsRequired().HasMaxLength(250);
                e.Property(t => t.PasswordSalt).IsRequired().HasMaxLength(250);
                e.HasIndex(t => t.Username).IsUnique();

                e.HasOne(t => t.RefreshToken)
                    .WithOne(t => t.Trainer)
                    .HasForeignKey<Trainer>(t => t.TokenId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
