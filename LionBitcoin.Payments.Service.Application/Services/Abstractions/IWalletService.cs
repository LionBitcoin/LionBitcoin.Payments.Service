using System.Threading.Tasks;

namespace LionBitcoin.Payments.Service.Application.Services.Abstractions;

public interface IWalletService
{
    string GenerateAddress();

    Task<string> GenerateAddressAsync();
}