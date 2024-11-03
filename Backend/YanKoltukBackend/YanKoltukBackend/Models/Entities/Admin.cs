namespace YanKoltukBackend.Models.Entities
{
    public class Admin : User
    {
        public ICollection<Manager> Managers { get; set; } = [];
    }
}