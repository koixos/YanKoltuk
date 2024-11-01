namespace YanKoltukBackend.Models
{
    public class Manager : User
    {
        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
