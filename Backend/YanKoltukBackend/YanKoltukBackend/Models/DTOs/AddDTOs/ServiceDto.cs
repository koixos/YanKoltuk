namespace YanKoltukBackend.Models.DTOs.AddDTOs
{
    public class ServiceDto
    {
        public required string Plate { get; set; }
        public int Capacity { get; set; }
        public string? DepartureLocation { get; set; }
        public string? DepartureTime { get; set; }

        public string? DriverIdNo { get; set; }
        public string? DriverName { get; set; }
        public string? DriverPhone { get; set; }
        public string? DriverPhoto { get; set; }

        public string? StewardessIdNo { get; set; }
        public string? StewardessName { get; set; }
        public string? StewardessPhone { get; set; }
        public string? StewardessPhoto { get; set; }
    }
}
