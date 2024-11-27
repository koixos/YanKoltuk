using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YanKoltukBackend.Models.Entities
{
    public class StudentService
    {
        public int StudentServiceId { get; set; }
        public bool Attended { get; set; } = true;
        public StudentStatus GetOff_GetOn { get; set; } = StudentStatus.GetOff;
        public string? DriverNote { get; set; }
        public int SortIndex { get; set; } = 0;
        public TripType? Direction { get; set; }

        [Required]
        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public Service? Service { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student? Student { get; set; }
    }

    public enum StudentStatus
    {
        [Description("İndi")]
        GetOff,

        [Description("Bindi")]
        GetOn
    }

    public enum TripType
    {
        [Description("Gidiş")]
        ToSchool,

        [Description("Dönüş")]
        FromSchool
    }
}
