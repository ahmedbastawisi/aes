using System.Text;

namespace Crypto.Net.Cryptography;

public static class DecodeHandler
{
    public static string Base64(string text)
    {
        var bytes =  Convert.FromBase64String(text);

        return Encoding.UTF8.GetString(bytes);
    }

    public static string Hex(string text)
    {
        var bytes = Convert.FromHexString(text);

        return Encoding.UTF8.GetString(bytes);
    }
}