namespace LionBitcoin.Payments.Service.Application.Features.Wallet.GenerateHdWallet;

public class GenerateHdWalletResponse
{
    public string RootPublicKey { get; set; }

    public string RootPrivateKey { get; set; }

    public string Mnemonic { get; set; }
}