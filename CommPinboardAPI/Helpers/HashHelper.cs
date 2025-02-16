using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace CommPinboardAPI.Helpers
{
    public class HashHelper
    {
        public async Task<string> ComputeSHA256(string input)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hashBytes); // .NET 5+
        }

        public async Task<string> Encrypt(string input){
            return BCrypt.Net.BCrypt.HashPassword(input);
        }

        public async Task<bool> Verify(string input, string hashed){
            return BCrypt.Net.BCrypt.Verify(input, hashed);
        }
    }
}