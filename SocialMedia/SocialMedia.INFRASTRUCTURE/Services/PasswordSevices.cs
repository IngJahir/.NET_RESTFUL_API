
namespace SocialMedia.INFRASTRUCTURE.Services
{
    using Microsoft.Extensions.Options;
    using SocialMedia.INFRASTRUCTURE.Interfaces;
    using SocialMedia.INFRASTRUCTURE.Options;
    using System;
    using System.Security.Cryptography;

    class PasswordSevices : IPasswordHasher
    {
        private readonly PasswordOptions _options;

        public PasswordSevices(IOptions<PasswordOptions> options)
        {
            _options = options.Value;
        }

        public bool Check(string hash, string password)
        {
            throw new NotImplementedException();
        }

        public string Hash(string password)
        {
            // PBKDF 2 implementation
            using (var algorithm = new Rfc2898DeriveBytes(password, _options.SaltSize, _options.Iterations))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_options.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{_options.Iterations}.{salt}.{key}";
            }
        }
    }
}
