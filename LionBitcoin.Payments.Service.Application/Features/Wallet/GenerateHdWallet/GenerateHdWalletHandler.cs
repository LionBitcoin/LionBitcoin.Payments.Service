using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using LionBitcoin.Payments.Service.Application.Services.Requests;
using MediatR;

namespace LionBitcoin.Payments.Service.Application.Features.Wallet.GenerateHdWallet;

public class GenerateHdWalletHandler
    : IRequestHandler<GenerateHdWalletQuery, GenerateHdWalletResponse>
{
    private readonly IWalletService _walletService;

    public GenerateHdWalletHandler(IWalletService walletService)
    {
        _walletService = walletService;
    }
    public Task<GenerateHdWalletResponse> Handle(GenerateHdWalletQuery request, CancellationToken cancellationToken)
    {
        Shared.Models.Wallet wallet = _walletService.Generate(new GenerateWalletRequest()
        {
            WordCount = request.WordCount
        });
        return Task.FromResult(new GenerateHdWalletResponse()
        {
            RootPrivateKey = wallet.RootPrivateKey,
            RootPublicKey = wallet.RootPublicKey,
            Mnemonic = wallet.Mnemonic
        });
    }
}