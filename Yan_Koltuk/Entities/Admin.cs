namespace Yan_Koltuk.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public List<Servis> Servisler { get; set; }

        public Admin()
        {
            Servisler = new List<Servis>();
        }
    }
}
