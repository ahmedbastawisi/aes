using System.Security.Cryptography;

string original = "hello, world!";

// Create a new instance of the Aes
// class.  This generates a new key and initialization
// vector (IV).
using (Aes aes = Aes.Create())
{
    // Display the aes info.
    Console.WriteLine(aes.ToString() + Environment.NewLine);
    Console.WriteLine("Key:        {0}", Convert.ToBase64String(aes.Key));
    Console.WriteLine("Iv:         {0}", Convert.ToBase64String(aes.IV));
    Console.WriteLine("KeySize:    {0}", aes.KeySize);
    Console.WriteLine("BlockSize:  {0}", aes.BlockSize);

    // Encrypt the text to base64 string.
    var encrypted = EncryptString(original, aes.Key, aes.IV);

    // Convert encrypted text to an array of bytes
    var encryptedbytes = Convert.FromBase64String(encrypted);

    // Decrypt the bytes to a string.
    string decrypted = DecryptString(encryptedbytes, aes.Key, aes.IV);

    //Display the original data and the decrypted data.
    Console.WriteLine("Original:   {0}", original);
    Console.WriteLine("Encrypted:  {0}", encrypted);
    Console.WriteLine("Decrypted:  {0}", decrypted);
}

static string EncryptString(string plainText, byte[] Key, byte[] IV)
{
    byte[] encrypted;

    // Create an Aes object
    // with the specified key and IV.
    using (Aes aes = Aes.Create())
    {
        aes.Key = Key;
        aes.IV = IV;

        // Create an encryptor to perform the stream transform.
        ICryptoTransform encryptor = aes.CreateEncryptor();

        // Create the streams used for encryption.
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        using (var sw = new StreamWriter(cs))
        {
            //Write all data to the stream.
            sw.Write(plainText);
        }
        encrypted = ms.ToArray();
    }

    // Return the encrypted bytes from the memory stream as base64 string.
    return Convert.ToBase64String(encrypted);
}

static string DecryptString(byte[] ciphertext, byte[] Key, byte[] IV)
{
    string plaintext = null;

    // Create an Aes object with the specified key and IV.
    using (Aes aes = Aes.Create())
    {
        aes.Key = Key;
        aes.IV = IV;

        // Create a decryptor to perform the stream transform.
        ICryptoTransform decryptor = aes.CreateDecryptor();

        // Create the streams used for decryption.
        using var ms = new MemoryStream(ciphertext);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);

        // Read the decrypted bytes from the decrypting stream
        plaintext = sr.ReadToEnd();
    }
    return plaintext;
}