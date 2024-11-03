namespace YanKoltukBackend.Models.Entities
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
        public TimeSpan? DropOffTime { get; set; }

        public TripType TripType { get; set; }
    }

    public enum TripType
    {
        ToSchool,
        FromSchool
    }
}
