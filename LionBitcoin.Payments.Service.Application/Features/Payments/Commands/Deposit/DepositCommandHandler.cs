using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace LionBitcoin.Payments.Service.Application.Features.Payments.Commands.Deposit;

public class DepositCommandHandler : IRequestHandler<DepositCommand, DepositResponse>
{
    public Task<DepositResponse> Handle(DepositCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}