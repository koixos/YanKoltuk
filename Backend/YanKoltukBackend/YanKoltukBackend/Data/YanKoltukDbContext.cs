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
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Stewardess> Stewardesses { get; set; }
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
            modelBuilder.Entity<Driver>().ToTable("Driver");
            modelBuilder.Entity<Stewardess>().ToTable("Stewardess");
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<Parent>().ToTable("Parent");
            modelBuilder.Entity<ServiceLog>().ToTable("ServiceLog");
            modelBuilder.Entity<StudentService>().ToTable("StudentService");

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
