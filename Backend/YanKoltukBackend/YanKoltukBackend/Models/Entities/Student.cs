namespace YanKoltukBackend.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public required int IdNo { get; set; }
        public required string Name { get; set; }
        public required string SchoolNo { get; set; }

        public required int ParentId { get; set; }
        public required Parent Parent { get; set; }

        public ICollection<StudentService> StudentServices { get; set; } = new List<StudentService>();
        public ICollection<ServiceLog> ServiceLogs { get; set; } = new List<ServiceLog>();
    }
}
