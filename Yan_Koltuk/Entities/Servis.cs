using Microsoft.Extensions.Hosting;
using System.Drawing;

namespace Yan_Koltuk.Entities
{
    public class Servis
    {
        public int Id { get; set; }
        public string Plaka { get; set; }
        public Sofor Sofor { get; set; }
        public Hostes Hostes { get; set; }
        public DateTime KalkisSaati { get; set; }
        public int Kapasite { get; set; }
        public string KalkisNoktasi { get; set; }
        public List<Ogrenci> Ogrenciler { get; set; }
        public List<string> LogFile { get; set; }

        public Servis()
        {
            Ogrenciler = new List<Ogrenci>();
            LogFile = new List<string>();
        }
    }
}
