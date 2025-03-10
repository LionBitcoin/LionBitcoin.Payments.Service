using System.Data;
using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Repositories;
using LionBitcoin.Payments.Service.Application.Repositories.Base;
using MediatR;

namespace LionBitcoin.Payments.Service.Application.Features.Customers.Commands.RegisterCustomer;

public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, RegisterCustomerResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;

    public RegisterCustomerCommandHandler(IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
    {
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
    }

    public async Task<RegisterCustomerResponse> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        using IDbTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        //TODO: ProduceCustomerCreatedEvent

        return new RegisterCustomerResponse();
    }
}