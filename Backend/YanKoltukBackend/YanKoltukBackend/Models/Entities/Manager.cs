using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YanKoltukBackend.Models.Entities
{
    public class Manager
    {
        public int ManagerId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [ForeignKey("Admin")]
        public int AdminId { get; set; }
        public Admin? Admin { get; set; }

        public ICollection<Service> Services { get; set; } = [];
    }
}
