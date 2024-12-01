using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YanKoltukBackend.Models.Entities
{
    [Index(nameof(Plate), IsUnique = true)]
    [Index(nameof(DriverIdNo), IsUnique = true)]
    [Index(nameof(StewardessIdNo), IsUnique = true)]
    public class Service
    {
        public int ServiceId { get; set; }
        [Required]
        public string? Plate { get; set; }
        public int Capacity { get; set; }
        public string? DepartureLocation { get; set; }
        public string? DepartureTime { get; set; }

        public string? DriverIdNo { get; set; }
        public string? DriverName { get; set; }
        public string? DriverPhone { get; set; }
        public string? DriverPhoto { get; set; }

        public string? StewardessIdNo { get; set; }
        public string? StewardessName { get; set; }
        public string? StewardessPhone { get; set; }
        public string? StewardessPhoto { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [ForeignKey("Manager")]
        public int ManagerId { get; set; }
        public Manager? Manager { get; set; }

        public ICollection<StudentService> StudentServices { get; set; } = [];
    }
}
