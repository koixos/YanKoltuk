namespace YanKoltukBackend.Models.DTOs.AddDTOs
{
    public class StudentDto
    {
        public required string IdNo { get; set; }
        public required string Name { get; set; }
        public required string SchoolNo { get; set; }
        public required string Plate { get; set; }
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
    }
}
