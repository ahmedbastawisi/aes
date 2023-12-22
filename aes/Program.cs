using Crypto.Net.Settings;
using Crypto.Net.Cryptography;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var settings = configuration.GetRequiredSection("Settings").Get<Settings>();

Console.WriteLine("Welcome to Crypto.Net" + Environment.NewLine);
Console.Write("Enter text to be encrypted: ");
Console.ForegroundColor = ConsoleColor.DarkGreen;

var original = Console.ReadLine();

Console.ResetColor();
Console.WriteLine();

// Display the aes info.
Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.WriteLine("System.Security.Cryptography.Aes"+ Environment.NewLine);
Console.ResetColor();
Console.WriteLine("Key:        {0}", settings.Aes.Key);
Console.WriteLine("IV:         {0}", settings.Aes.IV);
Console.WriteLine("Mode:       {0}", settings.Aes.Mode);
Console.WriteLine("KeySize:    {0}", settings.Aes.KeySize);
Console.WriteLine("BlockSize:  {0}", settings.Aes.BlockSize);

// Encrypt the plain text to encrypted base64 string.
var encrypted = AesEncryptor.Encrypt(original, settings.Aes.Key, settings.Aes.IV);

// Decrypt the encrypted base64 string back to plain text.
var decrypted = AesEncryptor.Decrypt(encrypted, settings.Aes.Key, settings.Aes.IV);

// Display the original data and the decrypted data.
Console.ForegroundColor = ConsoleColor.DarkGreen;
Console.WriteLine("Original:   {0}", original);
Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.WriteLine("Encrypted:  {0}", encrypted);
Console.ForegroundColor = ConsoleColor.DarkGreen;
Console.WriteLine("Decrypted:  {0}", decrypted);
Console.ResetColor();