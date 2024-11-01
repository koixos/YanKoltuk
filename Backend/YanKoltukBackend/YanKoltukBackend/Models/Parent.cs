namespace YanKoltukBackend.Models
{
    public class Parent : User
    {
        public required int IdNo { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
