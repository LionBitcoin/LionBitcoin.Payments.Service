using LionBitcoin.Payments.Service.Application.Shared.Enums;
using LionBitcoin.Payments.Service.Application.Services.Requests;
using LionBitcoin.Payments.Service.Application.Shared.Models;

namespace LionBitcoin.Payments.Service.Application.Services.Abstractions;

public interface IWalletService
{
    Wallet Generate(GenerateWalletRequest request);
    Task<Balance> CheckBalance(CheckBalanceRequest request, CancellationToken cancellationToken);
}