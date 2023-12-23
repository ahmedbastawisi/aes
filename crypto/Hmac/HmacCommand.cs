using System.CommandLine;
using Crypto.Net.Configuration;

namespace Crypto.Net.Cryptography;

public static class HmacCommand
{
    public static void AddHmacCommand(this RootCommand rootCommand, Settings settings)
    {
        var sha256Command = new Command("hmac", "HMAC SHA-256 cryptographic hash function.");

        sha256Command.AddHmacHashCommand(settings);

        rootCommand.AddCommand(sha256Command);
    }

    private static void AddHmacHashCommand(this Command parentCommand, Settings settings)
    {
        var textArgument = new Argument<string>(
            name: "text",
            description: "Input text to be hashed.");

        var hashCommand = new Command("hash", "Hash text using hmac sha256.")
        {
            textArgument
        };
        parentCommand.AddCommand(hashCommand);

        hashCommand.SetHandler((text) =>
        {
            var hashed = HmacHandler.Hash(text, settings.Hmac.Key);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(hashed);
            Console.ResetColor();

        }, textArgument);
    }
}