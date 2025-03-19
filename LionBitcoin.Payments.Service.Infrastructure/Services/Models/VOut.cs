using System.Text.Json.Serialization;

namespace LionBitcoin.Payments.Service.Infrastructure.Services.Models;

public class VOut
{
    [JsonPropertyName("scriptpubkey")]
    public string ScriptPubKey { get; set; }

    [JsonPropertyName("scriptpubkey_asm")]
    public string LockingScript { get; set; }

    [JsonPropertyName("scriptpubkey_type")]
    public string LockingScriptType { get; set; }

    [JsonPropertyName("scriptpubkey_address")]
    public string Address { get; set; }

    [JsonPropertyName("value")]
    public long Amount { get; set; }
}