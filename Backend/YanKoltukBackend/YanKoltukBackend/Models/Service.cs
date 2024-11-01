namespace YanKoltukBackend.Models
{
    public class Service : User
    {
        public required string Plate { get; set; }
        public int Capacity { get; set; }
        public string DepartureLocation { get; set; }
        public string DepartureTime { get; set; }

        public int DriverId { get; set; }
        public required Driver Driver { get; set; }

        public int StewardessId { get; set; }
        public required Stewardess Stewardess { get; set; }

        public ICollection<StudentService> StudentServices { get; set; } = new List<StudentService>();
        public ICollection<ServiceLog> ServiceLogs { get; set; } = new List<ServiceLog>();
    }
}
