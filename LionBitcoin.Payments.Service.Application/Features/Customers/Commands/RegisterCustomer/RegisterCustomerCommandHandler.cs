using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace LionBitcoin.Payments.Service.Application.Features.Customers.Commands.RegisterCustomer;

public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, RegisterCustomerResponse>
{
    public Task<RegisterCustomerResponse> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}