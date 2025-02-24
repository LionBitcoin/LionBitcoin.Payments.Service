using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace LionBitcoin.Payments.Service.Application.Features.Payments.Commands.Withdraw;

public class WithdrawCommandHandler : IRequestHandler<WithdrawCommand, WithdrawResponse>
{
    public Task<WithdrawResponse> Handle(WithdrawCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}