namespace YanKoltukBackend.Models.DTOs.UserDTOs
{
    public class ParentSignupDto
    {
        public string? Name { get; set; }
        public string? IdNo { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Address { get; set; }
    }
}
