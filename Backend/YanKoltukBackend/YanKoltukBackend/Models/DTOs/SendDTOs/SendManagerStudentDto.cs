namespace YanKoltukBackend.Models.DTOs.SendDTOs
{
    public class SendManagerStudentDto
    {
        public required int StudentId { get; set; }
        public required string IdNo { get; set; }
        public required string Name { get; set; }
        public required string SchoolNo { get; set; }
        public required string ParentName { get; set; }
        public required string ParentPhone { get; set; }
        public required string Address { get; set; }
    }
}
