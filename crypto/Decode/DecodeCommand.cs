using System.CommandLine;
using Crypto.Net.Configuration;

namespace Crypto.Net.Cryptography;

public static class DecodeCommand
{
    public static void AddDecodeCommand(this RootCommand rootCommand, Settings settings)
    {
        var decCommand = new Command("dec", "Convert from encoding formats such as Base64, Latin1 or Hex to UTF8");

        decCommand.AddDecodeBase64Command(settings);
        decCommand.AddDecodeHexCommand(settings);

        rootCommand.AddCommand(decCommand);
    }

    private static void AddDecodeBase64Command(this Command parentCommand, Settings settings)
    {
        var textArgument = new Argument<string>(
            name: "text",
            description: "Input text to be decoded from base64.");

        var hashCommand = new Command("base64", "Decoded from base64 string.")
        {
            textArgument
        };
        parentCommand.AddCommand(hashCommand);

        hashCommand.SetHandler((text) =>
        {
            var decoded = DecodeHandler.Base64(text);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(decoded);
            Console.ResetColor();

        }, textArgument);
    }

    private static void AddDecodeHexCommand(this Command parentCommand, Settings settings)
    {
        var textArgument = new Argument<string>(
            name: "text",
            description: "Input text to be decoded from hex.");

        var hashCommand = new Command("hex", "Decoded from hex string.")
        {
            textArgument
        };
        parentCommand.AddCommand(hashCommand);

        hashCommand.SetHandler((text) =>
        {
            var decoded = DecodeHandler.Hex(text);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(decoded);
            Console.ResetColor();

        }, textArgument);
    }
}