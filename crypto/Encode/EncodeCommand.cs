using System.CommandLine;
using Crypto.Net.Configuration;

namespace Crypto.Net.Cryptography;

public static class EncodeCommand
{
    public static void AddEncodeCommand(this RootCommand rootCommand, Settings settings)
    {
        var encCommand = new Command("enc", "Convert from UTF8 to encoding formats such as Base64, Latin1 or Hex.");

        encCommand.AddEncodeBase64Command(settings);
        encCommand.AddEncodeHexCommand(settings);

        rootCommand.AddCommand(encCommand);
    }

    private static void AddEncodeBase64Command(this Command parentCommand, Settings settings)
    {
        var textArgument = new Argument<string>(
            name: "text",
            description: "Input text to be encoded to base64.");

        var hashCommand = new Command("base64", "Encoded to base64 string.")
        {
            textArgument
        };
        parentCommand.AddCommand(hashCommand);

        hashCommand.SetHandler((text) =>
        {
            var encoded = EncodeHandler.Base64(text);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(encoded);
            Console.ResetColor();

        }, textArgument);
    }

    private static void AddEncodeHexCommand(this Command parentCommand, Settings settings)
    {
        var textArgument = new Argument<string>(
            name: "text",
            description: "Input text to be encoded to hex.");

        var hashCommand = new Command("hex", "Encoded to hex string.")
        {
            textArgument
        };
        parentCommand.AddCommand(hashCommand);

        hashCommand.SetHandler((text) =>
        {
            var encoded = EncodeHandler.Hex(text);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(encoded);
            Console.ResetColor();

        }, textArgument);
    }
}