namespace Yan_Koltuk.Entities
{
    public class Ogrenci
    {
        public int Id { get; set; }
        public string OgrenciNo { get; set; }
        public string AdSoyad { get; set; }
        public string TC { get; set; }
        public string DropOffPlaka { get; set; }
        public bool IndiBindiDurumu { get; set; } // True: Bindi, False: Indi
        public bool GelecekMi { get; set; } = true; // Default: gelecek
        public int ParentId { get; set; }
        public Parent Parent { get; set; }
        public string not { get; set; }
    }
}
