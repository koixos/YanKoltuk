using System.ComponentModel.DataAnnotations;

namespace YanKoltukBackend.Models.Entities
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string PasswordSalt { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
