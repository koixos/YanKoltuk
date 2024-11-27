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

        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public Service? Service { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
