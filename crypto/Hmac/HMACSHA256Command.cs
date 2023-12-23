using System.CommandLine;
using Crypto.Net.Configuration;

namespace Crypto.Net.Cryptography;

public static class HmacSha256Command
{
    public static void AddHmacSha256Command(this RootCommand rootCommand, Settings settings)
    {
        var sha256Command = new Command("hmac", "HMAC SHA-256 cryptographic hash function.");

        sha256Command.AddHmacSha256HashCommand(settings);

        rootCommand.AddCommand(sha256Command);
    }

    private static void AddHmacSha256HashCommand(this Command parentCommand, Settings settings)
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
            var hashed = HmacSha256Handler.Hash(text, "");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(hashed);
            Console.ResetColor();

        }, textArgument);
    }
}