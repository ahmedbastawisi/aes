using System.CommandLine;
using Crypto.Net.Configuration;

namespace Crypto.Net.Cryptography;

public static class AesCommand
{
    public static void AddAesCommand(this RootCommand rootCommand, Settings settings)
    {
        var aesCommand = new Command("aes", "Advanced encryption standard.");

        aesCommand.AddAesEncryptCommand(settings);
        aesCommand.AddAesDecryptCommand(settings);

        rootCommand.AddCommand(aesCommand);
    }

    private static void AddAesEncryptCommand(this Command parentCommand, Settings settings)
    {
        var textArgument = new Argument<string>(
            name: "plaintext",
            description: "Input plaintext to be encrypt.");

        var encryptCommand = new Command("encrypt", "Encrypt plaintext using aes.")
        {
            textArgument
        };
        parentCommand.AddCommand(encryptCommand);

        encryptCommand.SetHandler((plaintext) =>
        {
            var encrypted = AesHandler.Encrypt(plaintext, settings.Aes.Key, settings.Aes.IV);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(encrypted);
            Console.ResetColor();

        }, textArgument);
    }

    private static void AddAesDecryptCommand(this Command parentCommand, Settings settings)
    {
        var textArgument = new Argument<string>(
            name: "ciphertext",
            description: "Input plaintext to be decrypt.");

        var decryptCommand = new Command("decrypt", "Decrypt ciphertext using aes.")
        {
            textArgument
        };
        parentCommand.AddCommand(decryptCommand);

        decryptCommand.SetHandler((ciphertext) =>
        {
            var decrypted = AesHandler.Decrypt(ciphertext, settings.Aes.Key, settings.Aes.IV);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(decrypted);
            Console.ResetColor();

        }, textArgument);
    }
}