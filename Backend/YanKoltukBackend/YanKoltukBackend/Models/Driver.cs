namespace YanKoltukBackend.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public required int IdNo { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public string Photo { get; set; }
    }
}
