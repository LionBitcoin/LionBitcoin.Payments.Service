using System.ComponentModel;
using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using LionBitcoin.Payments.Service.Application.Services.Requests;
using LionBitcoin.Payments.Service.Application.Shared.Enums;
using LionBitcoin.Payments.Service.Application.Shared.Models;
using LionBitcoin.Payments.Service.Application.Shared.Settings;
using Microsoft.Extensions.Options;
using NBitcoin;

namespace LionBitcoin.Payments.Service.Application.Services;

public class WalletService : IWalletService
{
    private readonly PaymentsProcessorSettings _paymentsProcessorSettings;

    public WalletService(IOptions<PaymentsProcessorSettings> paymentProcessingOptions)
    {
        _paymentsProcessorSettings = paymentProcessingOptions.Value;
    }

    
    public Wallet Generate(GenerateWalletRequest request)
    {
        Mnemonic mnemonic = new Mnemonic(Wordlist.English, (NBitcoin.WordCount)request.WordCount);
        string mnemonicAsString = mnemonic.ToString();

        ExtKey rootKey = mnemonic.DeriveExtKey();
        string rootPrivateKey = rootKey.ToString(GetNetwork());

        ExtPubKey rootPubKey = rootKey.Neuter(); // 'Neuter' generates public key from private key. we already have 'Root private key' so public key which is generated from 'Root
        // private key' is called 'Root public key'. THAT'S IT
        string rootPublicKey = rootPubKey.ToString(GetNetwork());

        return new Wallet()
        {
            RootPrivateKey = rootPrivateKey,
            RootPublicKey = rootPublicKey,
            Mnemonic = mnemonicAsString
        };
    }

    public Task<Balance> CheckBalance(CheckBalanceRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private Network GetNetwork()
    {
        if (_paymentsProcessorSettings.Network == BtcNetwork.Main) return Network.Main;

        if (_paymentsProcessorSettings.Network == BtcNetwork.Test) return Network.TestNet;

        throw new InvalidEnumArgumentException("Invalid network configuration passed into PaymentsProcessorSettings");
    }
}