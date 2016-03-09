using System;
using System.Security.Cryptography;

namespace Trackwane.AccessControl.Domain.Users
{
    public class Credentials
    {        
        public static Credentials Create(string password)
        {
            var hash = HashPassword(password);
            return new Credentials(hash.Salt, hash.Value);
        }

        public Credentials(byte[] salt, string password)
        {
            Salt = salt;
            Password = password;
        }

        public string Password { get; set; }

        public byte[] Salt { get; set; }

        public string ApiToken { get; set; } = Guid.NewGuid().ToString();

        public bool IsValid(string password)
        {
            return HashPassword(password, Salt) == Password;
        }

        private static string HashPassword(string password, byte[] salt)
        {
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 32)
            {
                IterationCount = 10000,
                Salt = salt
            };
            var hash = rfc2898DeriveBytes.GetBytes(20);
            return Convert.ToBase64String(hash);
        }

        private static PasswordHash HashPassword(string password)
        {
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 32) { IterationCount = 10000 };
            var hash = rfc2898DeriveBytes.GetBytes(20);
            var salt = rfc2898DeriveBytes.Salt;
            return new PasswordHash(salt, Convert.ToBase64String(hash));
        }

        private struct PasswordHash
        {
            public byte[] Salt { get; }

            public string Value { get; }

            public PasswordHash(byte[] salt, string value)
            {
                Salt = salt;
                Value = value;
            }
        }
    }
}
