namespace YanKoltukBackend.Models.Entities
{
    public class Manager : User
    {
        public ICollection<Service> Services { get; set; } = [];
    }
}
