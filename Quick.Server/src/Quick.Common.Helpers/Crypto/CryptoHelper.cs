using System.Security.Cryptography;
using System.Text;

namespace Quick.Common.Helpers.Crypto
{
    public static class CryptoHelper
    {
        public static byte[] HmacSha256(string message, string key)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            return HmacSha256(messageBytes, key);
        }

        public static byte[] HmacSha256(byte[] message, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            using var hmac = new HMACSHA256(keyBytes);
            return hmac.ComputeHash(message);
        }
    }
}
