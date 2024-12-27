using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace YanKoltukBackend.Shared.Helpers
{
    public class AuthHelper(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public static string GeneratePasswd(int len = 12)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!@#$%^&*()";
            StringBuilder passwd = new();
            Random random = new();

            for (int i = 0; i < len; i++)
            {
                int index = random.Next(validChars.Length);
                passwd.Append(validChars[index]);
            }

            return passwd.ToString();
        }

        public string GenerateJwtToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string CreateSalt()
        {
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPasswd(string passwd, string salt)
        {
            var combinedPasswd = Encoding.UTF8.GetBytes(passwd + salt);
            var hash = SHA256.HashData(combinedPasswd);
            return Convert.ToBase64String(hash);
        }

        public static bool VerifyPasswd(string _passwd, string _salt, string _hash)
        {
            var hashOfEnteredPasswd = HashPasswd(_passwd, _salt);
            return hashOfEnteredPasswd == _hash;
        }
    }
}
