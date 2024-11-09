namespace YanKoltukBackend.Models.Entities
{
    public class Service : User
    {
        public required string Plate { get; set; }
        public int Capacity { get; set; }
        public string DepartureLocation { get; set; }
        public string DepartureTime { get; set; }

        /*public int DriverId { get; set; }
        public Driver Driver { get; set; }

        public int StewardessId { get; set; }
        public Stewardess Stewardess { get; set; }*/

        public required string DriverIdNo { get; set; }
        public required string DriverName { get; set; }
        public required string DriverPhone { get; set; }
        public string? DriverPhoto { get; set; }

        public required string StewardessIdNo { get; set; }
        public required string StewardessName { get; set; }
        public required string StewardessPhone { get; set; }
        public string? StewardessPhoto { get; set; }

        public required int ManagerId { get; set; }
        public Manager Manager { get; set; }

        public ICollection<StudentService> StudentServices { get; set; } = [];
        public ICollection<ServiceLog> ServiceLogs { get; set; } = [];
    }
}
