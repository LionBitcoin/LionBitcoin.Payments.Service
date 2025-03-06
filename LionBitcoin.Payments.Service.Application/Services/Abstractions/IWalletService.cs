using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Services.Models;

namespace LionBitcoin.Payments.Service.Application.Services.Abstractions;

public interface IWalletService
{
    GenerateAddressResponse GenerateAddress(GenerateAddressRequest request);

    Task<GenerateAddressResponse> GenerateAddressAsync(GenerateAddressRequest request, CancellationToken cancellationToken = default);
}