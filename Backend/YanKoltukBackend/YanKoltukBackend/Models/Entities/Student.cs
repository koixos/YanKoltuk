using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YanKoltukBackend.Models.Entities
{
    [Index(nameof(IdNo), IsUnique = true)]
    [Index(nameof(SchoolNo), IsUnique = true)]
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        public string? IdNo { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? SchoolNo { get; set; }

        [Required]
        [ForeignKey("Parent")]
        public int ParentId { get; set; }
        public Parent? Parent { get; set; }

        public StudentService? StudentService { get; set; }
    }
}
