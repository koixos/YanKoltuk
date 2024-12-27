using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YanKoltukBackend.Models.Entities
{
    public class ServiceLog
    {
        public int ServiceLogId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? PickupTime { get; set; }
        public TimeSpan DropOffTime { get; set; }
        public string? Direction { get; set; }

        [Required]
        [ForeignKey("StudentService")]
        public int StudentServiceId { get; set; }
        public StudentService? StudentService { get; set; }
    }
}
