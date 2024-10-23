using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;


namespace Yan_Koltuk.Entities
{
    public class AppDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Servis> Servisler { get; set; }
        public DbSet<Sofor> Soforler { get; set; }
        public DbSet<Hostes> Hostesler { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Ogrenci> Ogrenciler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servis>()
                .HasOne(s => s.Sofor)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Servis>()
                .HasOne(s => s.Hostes)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Parent>()
                .HasMany(p => p.Ogrenciler)
                .WithOne(o => o.Parent)
                .HasForeignKey(o => o.ParentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
