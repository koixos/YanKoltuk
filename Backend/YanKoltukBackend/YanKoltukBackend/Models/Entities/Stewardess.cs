namespace YanKoltukBackend.Models.Entities
{
    public class Stewardess
    {
        public int Id { get; set; }
        public required int IdNo { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public string? Photo { get; set; }
    }
}
