using Crypto.Net.Configuration;
using Crypto.Net.Cryptography;
using Microsoft.Extensions.Configuration;
using System.CommandLine;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var settings = configuration.GetRequiredSection("Settings").Get<Settings>();

var rootCommand = new RootCommand("Welcome to Crypto.Net");

rootCommand.AddAesCommand(settings);

args = args.Length != 0 ? args : [.. args, "-h"];

await rootCommand.InvokeAsync(args);