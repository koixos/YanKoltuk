namespace YanKoltukBackend.Models.Entities
{
    public class StudentService
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public bool Attended { get; set; }
        public bool GetOff_GetOn { get; set; }
        public string DriverNote { get; set; }
        public string Direction { get; set; }
    }
}
