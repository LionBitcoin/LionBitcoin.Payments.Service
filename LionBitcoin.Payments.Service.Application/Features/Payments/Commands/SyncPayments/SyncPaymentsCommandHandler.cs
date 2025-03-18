using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using LionBitcoin.Payments.Service.Application.Shared;
using MediatR;
using Microsoft.Extensions.Options;

namespace LionBitcoin.Payments.Service.Application.Features.Payments.Commands.SyncPayments;

public class SyncPaymentsCommandHandler : IRequestHandler<SyncPaymentsCommand, SyncPaymentsResponse>
{
    private readonly IBlockExplorerService _blockExplorerService;
    private readonly PaymentServiceSettings _paymentsServiceSettings;

    public SyncPaymentsCommandHandler(
        IOptions<PaymentServiceSettings> paymentsServiceOptions,
        IBlockExplorerService blockExplorerService)
    {
        _blockExplorerService = blockExplorerService;
        _paymentsServiceSettings = paymentsServiceOptions.Value;
    }
    public async Task<SyncPaymentsResponse> Handle(SyncPaymentsCommand request, CancellationToken cancellationToken)
    {
        string blockHash =
            await _blockExplorerService.GetBlockHash(_paymentsServiceSettings.DefaultThresholdBlockIndex, cancellationToken);
        throw new System.NotImplementedException();
    }
}