namespace YanKoltukBackend.Models.DTOs
{
    public class ServiceDto
    {
        public required string Plate { get; set; }
        public int Capacity { get; set; }
        public required string DepartureLocation { get; set; }
        public required string DepartureTime { get; set; }

        public string DriverIdNo { get; set; }
        public required string DriverName { get; set; }
        public required string DriverPhone { get; set; }
        public required string DriverPhoto { get; set; }

        public string StewardessIdNo { get; set; }
        public required string StewardessName { get; set; }
        public required string StewardessPhone { get; set; }
        public required string StewardessPhoto { get; set; }
    }
}
