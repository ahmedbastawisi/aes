using System.Text;

namespace Crypto.Net.Cryptography;

public static class EncodeHandler
{
    public static string Base64(string text)
    {
        var bytes =  Encoding.UTF8.GetBytes(text);

        return Convert.ToBase64String(bytes);
    }

    public static string Hex(string text)
    {
        var bytes = Encoding.UTF8.GetBytes(text);

        return Convert.ToHexString(bytes);
    }
}