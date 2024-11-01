using Microsoft.EntityFrameworkCore;
using YanKoltukBackend.Models;

namespace YanKoltukBackend.YanKoltukDb
{
    public class YanKoltukDbContext : DbContext
    {
        public YanKoltukDbContext(DbContextOptions<YanKoltukDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
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
