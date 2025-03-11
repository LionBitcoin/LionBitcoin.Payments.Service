using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Domain.Enums;
using LionBitcoin.Payments.Service.Application.Domain.Exceptions.Base;
using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using LionBitcoin.Payments.Service.Application.Services.Enums;
using LionBitcoin.Payments.Service.Application.Services.Models;
using LionBitcoin.Payments.Service.Application.Shared;
using Microsoft.Extensions.Options;
using NBitcoin;

namespace LionBitcoin.Payments.Service.Application.Services;

public class HdWalletService : IWalletService
{
    private readonly IOptions<PaymentServiceSettings> _paymentServiceSettingsOptions;
    public HdWalletService(
        IOptions<PaymentServiceSettings> paymentServiceSettingsOptions)
    {
        _paymentServiceSettingsOptions = paymentServiceSettingsOptions;
    }

    public GenerateAddressResponse GenerateAddress(GenerateAddressRequest request)
    {
        // Evaluate BTC network
        NBitcoin.Network btcNetwork = _paymentServiceSettingsOptions.Value.Network == Shared.Network.Main
            ? NBitcoin.Network.Main
            : NBitcoin.Network.TestNet4;

        // Load the extended public key
        ExtPubKey extPubKey = ExtPubKey.Parse(_paymentServiceSettingsOptions.Value.AccountExtendedPublicKey, btcNetwork);
        
        KeyPath derivationPath = request.AddressType switch
        {
            AddressType.Receiving => new KeyPath($"0/{request.AddressIndex}"),
            AddressType.Change => new KeyPath($"1/{request.AddressIndex}"),
            _ => throw new PaymentServiceException(ExceptionType.AddressTypeDoesNotExist)
        };

        // Derive public key based on derivation path
        PubKey addressPubKey = extPubKey.Derive(derivationPath).PubKey;

        // Generate SegWit (Segregated Witness) standard address
        BitcoinAddress address = addressPubKey.GetAddress(ScriptPubKeyType.Segwit, btcNetwork);

        return new GenerateAddressResponse(Address: address.ToString(), derivationPath.ToString());
    }
}