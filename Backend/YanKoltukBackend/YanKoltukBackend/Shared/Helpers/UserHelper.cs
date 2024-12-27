using System.Security.Claims;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Shared.Helpers
{
    public class UserHelper(IHttpContextAccessor httpContextAccessor)
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public User CreateUser(string username, string passwd, string role)
        {
            var salt = AuthHelper.CreateSalt();
            var hashedPasswd = AuthHelper.HashPasswd(passwd, salt);

            return new User
            {
                Username = username,
                PasswordHash = hashedPasswd,
                PasswordSalt = salt,
                Role = role
            };
        }

        public int GetUserId()
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return userId;
        }
    }
}
