using Microsoft.AspNet.Identity;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Site.Identity
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return password.GetMD5Hash();
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            return hashedPassword == HashPassword(providedPassword) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
    public static class PasswordHash
    {
        public static string GetMD5Hash(this string str)
        {
            return (str ?? string.Empty).GetMD5Hash(new UnicodeEncoding());
        }
        public static string GetShortMD5Hash(this string str)
        {
            return (str ?? string.Empty).GetShortMD5Hash(new UnicodeEncoding());
        }

        private static byte[] GetHash(string str, Encoding encoding)
        {
            var clearBytes = encoding.GetBytes(str ?? string.Empty);
            var form = CryptoConfig.CreateFromName("MD5");
            var algoritm = (HashAlgorithm)form;

            return algoritm.ComputeHash(clearBytes);
        }

        public static string GetMD5Hash(this string str, Encoding encoding)
        {
            var hash = GetHash(str, encoding);
            return BitConverter.ToString(hash);
        }

        public static string GetShortMD5Hash(this string str, Encoding encoding)
        {
            var hash = GetHash(str, encoding);

            var builder = new StringBuilder();
            for (int index = 0; index < hash.Length; index++)
                builder.Append(hash[index].ToString("X2"));

            return builder.ToString();
        }
    }
}
