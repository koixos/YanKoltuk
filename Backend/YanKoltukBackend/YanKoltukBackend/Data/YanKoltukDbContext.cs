using Microsoft.EntityFrameworkCore;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Data
{
    public class YanKoltukDbContext(DbContextOptions<YanKoltukDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceLog> ServiceLogs { get; set; }
        public DbSet<StudentService> StudentServices { get; set; }

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

            // User-Admin Relationships
            modelBuilder.Entity<Admin>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User-Manager Relationships
            modelBuilder.Entity<Manager>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User-Service Relationships
            modelBuilder.Entity<Service>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User-Parent Relationships
            modelBuilder.Entity<Parent>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Manager-Service Relationships
            modelBuilder.Entity<Service>()
                .HasOne(s => s.Manager)
                .WithMany(m => m.Services)
                .HasForeignKey(s => s.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student-Service Relationships
            modelBuilder.Entity<StudentService>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentServices)
                .HasForeignKey(ss => ss.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentService>()
                .HasOne(ss => ss.Service)
                .WithMany(s => s.StudentServices)
                .HasForeignKey(ss => ss.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // ServiceLog Relationships
            modelBuilder.Entity<ServiceLog>()
                .HasOne(sl => sl.Service)
                .WithMany(s => s.ServiceLogs)
                .HasForeignKey(sl => sl.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceLog>()
                .HasOne(sl => sl.Student)
                .WithMany(s => s.ServiceLogs)
                .HasForeignKey(sl => sl.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
