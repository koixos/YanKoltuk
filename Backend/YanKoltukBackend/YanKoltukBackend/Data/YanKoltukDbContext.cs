using Microsoft.EntityFrameworkCore;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Data
{
    public class YanKoltukDbContext(DbContextOptions<YanKoltukDbContext> options) : DbContext(options)
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Admin>? Admins { get; set; }
        public DbSet<Manager>? Managers { get; set; }
        public DbSet<Student>? Students { get; set; }
        public DbSet<Parent>? Parents { get; set; }
        public DbSet<Service>? Services { get; set; }
        public DbSet<ServiceLog>? ServiceLogs { get; set; }
        public DbSet<StudentService>? StudentServices { get; set; }
        public DbSet<ParentNotification>? ParentNotifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Admin>().ToTable("Admin");
            modelBuilder.Entity<Manager>().ToTable("Manager");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<Parent>().ToTable("Parent");
            modelBuilder.Entity<ServiceLog>().ToTable("ServiceLog");
            modelBuilder.Entity<StudentService>().ToTable("StudentService");
            modelBuilder.Entity<ParentNotification>().ToTable("ParentNotification");

            ConfigureUserRelationships(modelBuilder);

            ConfigureEntityRelationships(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigureUserRelationships(ModelBuilder modelBuilder)
        {
            // User-Admin Relationships
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User-Manager Relationships
            modelBuilder.Entity<Manager>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User-Service Relationships
            modelBuilder.Entity<Service>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User-Parent Relationships
            modelBuilder.Entity<Parent>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureEntityRelationships(ModelBuilder modelBuilder)
        {
            // Admin-Manager Relationships
            modelBuilder.Entity<Manager>()
                .HasOne(m => m.Admin)
                .WithMany(a => a.Managers)
                .HasForeignKey(m => m.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // Manager-Service Relationships
            modelBuilder.Entity<Service>()
                .HasOne(s => s.Manager)
                .WithMany(m => m.Services)
                .HasForeignKey(s => s.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student-StudentService Relationship
            modelBuilder.Entity<Student>()
                .HasOne(s => s.StudentService)
                .WithOne(ss => ss.Student)
                .HasForeignKey<StudentService>(ss => ss.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Service-StudentService Relationship
            modelBuilder.Entity<Service>()
                .HasMany(s => s.StudentServices)
                .WithOne(ss => ss.Service)
                .HasForeignKey(ss => ss.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // StudentService-ServiceLog Relationships
            modelBuilder.Entity<StudentService>()
                .HasMany(ss => ss.ServiceLogs)
                .WithOne(sl => sl.StudentService)
                .HasForeignKey(sl => sl.StudentServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Parent-ParentNotification Relationships
            modelBuilder.Entity<ParentNotification>()
                .HasOne(pf => pf.Parent)
                .WithMany(p => p.Notifications)
                .HasForeignKey(pf => pf.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
