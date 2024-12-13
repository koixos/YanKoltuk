namespace YanKoltukBackend.Models.DTOs.SendDTOs
{
    public class SendStudentDto
    {
        public required int StudentId { get; set; }
        public required string IdNo { get; set; }
        public required string Name { get; set; }
        public required string SchoolNo { get; set; }
        public required string Plate { get; set; }
        public required string Status { get; set; }
        public required string DriverNote { get; set; }
        public required int SortIndex { get; set; }
        public required string? Direction { get; set; }
        public required DateTime? ExcludedStartDate { get; set; }
        public required DateTime? ExcludedEndDate { get; set; }
    }
}
