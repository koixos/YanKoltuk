using System.ComponentModel;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Shared.Helpers
{
    public class UserHelper
    {
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
    }
}
