using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YanKoltukBackend.Models.Entities
{
    public class ExcludedDate
    {
        public int ExcludedDateId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        [ForeignKey("StudentService")]
        public int StudentServiceId { get; set; }
        public StudentService? StudentService { get; set; }
    }
}
