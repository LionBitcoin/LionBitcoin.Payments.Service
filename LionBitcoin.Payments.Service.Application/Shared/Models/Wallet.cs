namespace LionBitcoin.Payments.Service.Application.Shared.Models;

public class Wallet
{
    public string RootPublicKey { get; set; }

    public string RootPrivateKey { get; set; }

    public string Mnemonic { get; set; }
}