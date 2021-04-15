using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Skrabbl.API.Services
{
    public class CryptographyService : ICryptographyService
    {
        public byte[] CreateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var salt = new byte[16];
                rng.GetBytes(salt);

                return salt;
            }
        }

        public string GenerateHash(string input, byte[] salt)
        {
            var hash = KeyDerivation.Pbkdf2(
                password: input,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 32
            );

            return Convert.ToBase64String(hash);
        }

        public bool AreEqual(string plainTextInput, string hashedInput, byte[] salt)
        {
            var newHashedPin = GenerateHash(plainTextInput, salt);

            return newHashedPin.Equals(hashedInput);
        }
    }
}