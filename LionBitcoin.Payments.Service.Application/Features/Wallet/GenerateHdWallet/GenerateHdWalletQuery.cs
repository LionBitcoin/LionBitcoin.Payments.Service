using LionBitcoin.Payments.Service.Application.Shared.Enums;
using MediatR;

namespace LionBitcoin.Payments.Service.Application.Features.Wallet.GenerateHdWallet;

public class GenerateHdWalletQuery : IRequest<GenerateHdWalletResponse>
{
    public WordCount WordCount { get; set; }
}