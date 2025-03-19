using System.Text.Json.Serialization;

namespace LionBitcoin.Payments.Service.Infrastructure.Services.Models;

public class TxInfoFromMempoolSpace
{
    [JsonPropertyName("vout")]
    public VOut[] Outputs { get; set; }
}