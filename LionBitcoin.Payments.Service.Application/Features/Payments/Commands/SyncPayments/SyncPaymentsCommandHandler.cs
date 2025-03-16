using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Shared;
using MediatR;
using Microsoft.Extensions.Options;

namespace LionBitcoin.Payments.Service.Application.Features.Payments.Commands.SyncPayments;

public class SyncPaymentsCommandHandler : IRequestHandler<SyncPaymentsCommand, SyncPaymentsResponse>
{
    private readonly PaymentServiceSettings _paymentsServiceSettings;

    public SyncPaymentsCommandHandler(IOptions<PaymentServiceSettings> paymentsServiceOptions)
    {
        _paymentsServiceSettings = paymentsServiceOptions.Value;
    }
    public Task<SyncPaymentsResponse> Handle(SyncPaymentsCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}