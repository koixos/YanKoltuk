using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YanKoltukBackend.Models.Entities
{
    public class ParentNotification
    {
        public int ParentNotificationId { get; set; }
        public int ServiceId { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public DateTime Date { get; set; }

        [Required]
        [ForeignKey("Parent")]
        public int ParentId { get; set; }
        public Parent? Parent { get; set; }
    }
}
