namespace YanKoltukBackend.Models.Entities
{
    public class Service : User
    {
        public required string Plate { get; set; }
        public int Capacity { get; set; }
        public string DepartureLocation { get; set; }
        public string DepartureTime { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        public int StewardessId { get; set; }
        public Stewardess Stewardess { get; set; }

        public ICollection<StudentService> StudentServices { get; set; } = [];
        public ICollection<ServiceLog> ServiceLogs { get; set; } = [];
    }
}
