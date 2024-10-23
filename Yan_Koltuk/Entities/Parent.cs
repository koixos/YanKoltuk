namespace Yan_Koltuk.Entities
{
    public class Parent
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
        public string TC { get; set; }
        public string Password { get; set; }
        public List<Ogrenci> Ogrenciler { get; set; }

        public Parent()
        {
            Ogrenciler = new List<Ogrenci>();
        }
    }
}
