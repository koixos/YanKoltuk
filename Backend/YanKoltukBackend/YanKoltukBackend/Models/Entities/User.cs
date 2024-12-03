using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YanKoltukBackend.Models.Entities
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? PasswordHash { get; set; }
        [Required]
        public string? PasswordSalt { get; set; }
        [Required]
        public string? Role { get; set; }
    }

    public enum Roles
    {
        [Description("Admin")]
        Admin,

        [Description("Manager")]
        Manager,

        [Description("Parent")]
        Parent,

        [Description("Service")]
        Service
    }
}
