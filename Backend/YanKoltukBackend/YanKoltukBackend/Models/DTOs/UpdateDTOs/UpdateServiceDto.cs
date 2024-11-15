namespace YanKoltukBackend.Models.DTOs.UpdateDTOs
{
    public class UpdateServiceDto
    {
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
