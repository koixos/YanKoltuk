using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Models.DTOs.SendDTOs
{
    public class SendServiceDto
    {
        public int ServiceId { get; set; }
        public string? Plate { get; set; }
        public int Capacity { get; set; }
        public string? DepartureLocation { get; set; }
        public string? DepartureTime { get; set; }

        public string? DriverIdNo { get; set; }
        public string? DriverName { get; set; }
        public string? DriverPhone { get; set; }

        public string? StewardessIdNo { get; set; }
        public string? StewardessName { get; set; }
        public string? StewardessPhone { get; set; }
    }
}
