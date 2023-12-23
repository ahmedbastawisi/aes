using System.Security.Cryptography;
using System.Text;

namespace Crypto.Net.Cryptography;

public static class HMACSHA256Handler
{
    public static string Hash(string text, string key)
    {
        var kb = Convert.FromBase64String(key);

        var source = Encoding.UTF8.GetBytes(text);
        var hash = HMACSHA256.HashData(kb, source);

        return Convert.ToBase64String(hash);
    }
}