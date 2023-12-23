using System.CommandLine;
using Crypto.Net.Configuration;

namespace Crypto.Net.Cryptography;

public static class AesCommand
{
    public static void AddAesEncryptCommand(this RootCommand rootCommand, Settings settings)
    {
        var textArgument = new Argument<string>(
            name: "plaintext",
            description: "Input plaintext to be encrypt.");

        var encryptCommand = new Command("encrypt", "Encrypt plaintext using aes.")
        {
            textArgument
        };
        rootCommand.AddCommand(encryptCommand);

        encryptCommand.SetHandler((plaintext) =>
        {
            var encrypted = AesHandler.Encrypt(plaintext, settings.Aes.Key, settings.Aes.IV);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(encrypted);
            Console.ResetColor();

        }, textArgument);
    }

    public static void AddAesDecryptCommand(this RootCommand rootCommand, Settings settings)
    {
        var textArgument = new Argument<string>(
            name: "ciphertext",
            description: "Input plaintext to be decrypt.");

        var decryptCommand = new Command("decrypt", "Decrypt ciphertext using aes.")
        {
            textArgument
        };
        rootCommand.AddCommand(decryptCommand);

        decryptCommand.SetHandler((ciphertext) =>
        {
            var decrypted = AesHandler.Decrypt(ciphertext, settings.Aes.Key, settings.Aes.IV);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(decrypted);
            Console.ResetColor();

        }, textArgument);
    }
}