namespace YanKoltukBackend.Models.DTOs
{
    public class ServiceDto
    {
        public string Plate { get; set; }
        public int Capacity { get; set; }
        public string DepartureLocation { get; set; }
        public string DepartureTime { get; set; }
        public int DriverId { get; set; }
        public int StewardessId { get; set; }
    }
}
