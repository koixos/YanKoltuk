using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YanKoltukBackend.Models.Entities
{
    [Index(nameof(IdNo), IsUnique = true)]
    [Index(nameof(Phone), IsUnique = true)]
    public class Parent
    {
        public int ParentId { get; set; }
        [Required]
        public string? IdNo { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Address { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Student> Students { get; set; } = [];
        public ICollection<ParentNotification> Notifications { get; set; } = [];
    }
}
