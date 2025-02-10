using LionBitcoin.Payments.Service.Application.Shared.Enums;

namespace LionBitcoin.Payments.Service.Application.Shared.Settings;

public class PaymentsProcessorSettings
{
    public string Mnemonic { get; set; }

    public BtcNetwork Network { get; set; }
}