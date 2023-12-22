namespace Crypto.Net.Settings;

public sealed class Settings
{
    public required Aes Aes { get; set; }

}

public sealed class Aes
{
    public required string Key { get; set; }
    public required string IV { get; set; }
    public required string Mode { get; set; }
    public required string KeySize { get; set; }
    public required string BlockSize { get; set; }
}