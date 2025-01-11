using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YanKoltukBackend.Models.Entities
{
    public class StudentService
    {
        public int StudentServiceId { get; set; }
        public StudentStatus Status { get; set; } = StudentStatus.Error;
        public string DriverNote { get; set; } = string.Empty;
        public int SortIndex { get; set; } = 0;
        public required double? Latitude { get; set; }
        public required double? Longitude { get; set; }
        public TripType Direction { get; set; } = TripType.Error;
        public DateTime? ExcludedStartDate { get; set; }
        public DateTime? ExcludedEndDate { get; set; }

        [ForeignKey("Service")]
        public int? ServiceId { get; set; }
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

        [Description("Bilgi Yok")]
        Error
    }

    public enum TripType
    {
        [Description("Okula Gidiş")]
        ToSchool,

        [Description("Okuldan Dönüş")]
        FromSchool,

        [Description("Bilgi Yok")]
        Error
    }
}
