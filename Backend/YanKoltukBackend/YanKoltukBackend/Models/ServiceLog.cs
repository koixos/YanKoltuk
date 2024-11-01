namespace YanKoltukBackend.Models
{
    public class ServiceLog
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan? PickupTime { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public TimeSpan? DropOffTime { get; set; }
        public TimeSpan? EndTime { get; set; }
    }
}
