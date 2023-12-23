using System.Security.Cryptography;
using System.Text;

namespace Crypto.Net.Cryptography;

public static class SHA256Handler
{
    public static string Hash(string text)
    {
        var source = Encoding.UTF8.GetBytes(text);
        var hash = SHA256.HashData(source);

        return Convert.ToBase64String(hash);
    }
}