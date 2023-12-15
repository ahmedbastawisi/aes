using System.Security.Cryptography;

var original = "hello, world!";

// Create a new instance of the Aes class.
// This generates a new key and initialization vector (IV).
using (var aes = Aes.Create())
{
    // Display the aes info.
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine(aes.ToString() + Environment.NewLine);
    Console.ResetColor();
    Console.WriteLine("Key:        {0}", Convert.ToBase64String(aes.Key));
    Console.WriteLine("Iv:         {0}", Convert.ToBase64String(aes.IV));
    Console.WriteLine("KeySize:    {0}", aes.KeySize);
    Console.WriteLine("BlockSize:  {0}", aes.BlockSize);

    // Encrypt the plain text to encrypted base64 string.
    var encrypted = EncryptString(original, aes.Key, aes.IV);

    // Decrypt the encrypted base64 string back to plain text.
    var decrypted = DecryptString(encrypted, aes.Key, aes.IV);

    //Display the original data and the decrypted data.
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Original:   {0}", original);
    Console.ResetColor();
    Console.WriteLine("Encrypted:  {0}", encrypted);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Decrypted:  {0}", decrypted);
    Console.ResetColor();

}

static string EncryptString(string plaintext, byte[] Key, byte[] IV)
{
    byte[] encrypted;

    // Create an Aes object with the specified key and IV.
    using (var aes = Aes.Create())
    {
        aes.Key = Key;
        aes.IV = IV;

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

static string DecryptString(string ciphertext, byte[] Key, byte[] IV)
{
    string plaintext = null;

    // Create an Aes object with the specified key and IV.
    using (var aes = Aes.Create())
    {
        aes.Key = Key;
        aes.IV = IV;

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