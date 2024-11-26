using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YanKoltukBackend.Models.Entities
{
    public class StudentService
    {
        public int StudentServiceId { get; set; }
        public bool Attended { get; set; }
        public bool GetOff_GetOn { get; set; }
        public string DriverNote { get; set; }
        public int SortIndex { get; set; }
        public string Direction { get; set; }

        [Required]
        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
