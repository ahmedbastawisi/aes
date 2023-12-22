using System.Security.Cryptography;

namespace Crypto.Net.Cryptography;

public static class AesEncryptor
{
    public static string Encrypt(string plaintext, string Key, string IV)
    {
        byte[] encrypted;

        // Create an Aes object with the specified key and IV.
        using (var aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(Key);
            aes.IV = Convert.FromBase64String(IV);

            // Create an encryptor to perform the stream transform.
            var encryptor = aes.CreateEncryptor();

            // Create the streams used for encryption.
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                //Write all data to the stream.
                sw.Write(plaintext);
            }
            encrypted = ms.ToArray();
        }
        // Return the encrypted bytes from the memory stream as base64 string.
        return Convert.ToBase64String(encrypted);
    }

    public static string Decrypt(string ciphertext, string Key, string IV)
    {
        string plaintext = null;

        // Create an Aes object with the specified key and IV.
        using (var aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(Key);
            aes.IV = Convert.FromBase64String(IV);

            // Convert cipher text to an array of bytes.
            var encrypted = Convert.FromBase64String(ciphertext);

            // Create a decryptor to perform the stream transform.
            var decryptor = aes.CreateDecryptor();

            // Create the streams used for decryption.
            using var ms = new MemoryStream(encrypted);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            // Read the decrypted bytes from the decrypting stream.
            plaintext = sr.ReadToEnd();
        }
        return plaintext;
    }
}

