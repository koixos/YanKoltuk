using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YanKoltukBackend.Models.Entities
{
    public class StudentService
    {
        public int StudentServiceId { get; set; }
        public StudentStatus Status { get; set; } = StudentStatus.GetOff;
        public string DriverNote { get; set; } = string.Empty;
        public int SortIndex { get; set; } = 0;
        public TripType? Direction { get; set; }
        public DateTime? ExcludedStartDate { get; set; } = null;
        public DateTime? ExcludedEndDate { get; set; } = null;

        [Required]
        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public Service? Service { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public ICollection<ServiceLog> ServiceLogs { get; set; } = [];
    }

    public enum StudentStatus
    {
        [Description("İndi")]
        GetOff,

        [Description("Bindi")]
        GetOn,

        [Description("Hata")]
        Error
    }

    public enum TripType
    {
        [Description("Okula Gidiş")]
        ToSchool,

        [Description("Okuldan Dönüş")]
        FromSchool,

        [Description("Hata")]
        Error
    }
}
