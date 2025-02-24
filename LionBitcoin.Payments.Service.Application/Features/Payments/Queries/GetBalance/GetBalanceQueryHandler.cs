using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace LionBitcoin.Payments.Service.Application.Features.Payments.Queries.GetBalance;

public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, GetBalanceResponse>
{
    public Task<GetBalanceResponse> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}