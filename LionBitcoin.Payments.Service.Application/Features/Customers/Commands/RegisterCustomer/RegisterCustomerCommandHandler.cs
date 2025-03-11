using System.Data;
using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Domain.Entities;
using LionBitcoin.Payments.Service.Application.Repositories;
using LionBitcoin.Payments.Service.Application.Repositories.Base;
using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using LionBitcoin.Payments.Service.Application.Services.Enums;
using LionBitcoin.Payments.Service.Application.Services.Models;
using MediatR;

namespace LionBitcoin.Payments.Service.Application.Features.Customers.Commands.RegisterCustomer;

public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, RegisterCustomerResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;
    private readonly ITimeProviderService _timeProvider;
    private readonly IWalletService _walletService;

    public RegisterCustomerCommandHandler(
        IUnitOfWork unitOfWork, 
        ICustomerRepository customerRepository,
        ITimeProviderService timeProvider,
        IWalletService walletService)
    {
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
        _timeProvider = timeProvider;
        _walletService = walletService;
    }

    public async Task<RegisterCustomerResponse> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        using IDbTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        
        Customer customer = InitiateNewCustomer(request);
        int customerId = await _customerRepository.Insert(customer, cancellationToken);

        GenerateAddressResponse generateAddressResponse = _walletService.GenerateAddress(new GenerateAddressRequest(customerId, AddressType.Receiving));

        await UpdateCustomerDepositInfo(customer, generateAddressResponse, cancellationToken);

        transaction.Commit();

        return new RegisterCustomerResponse()
        {
            Customer = customer
        };
    }

    private async Task UpdateCustomerDepositInfo(Customer customer, GenerateAddressResponse generateAddressResponse, CancellationToken cancellationToken)
    {
        customer.DepositAddress = generateAddressResponse.Address;
        customer.DepositAddressDerivationPath = generateAddressResponse.DerivationPath;
        await _customerRepository.Update(customer, cancellationToken);
    }

    private Customer InitiateNewCustomer(RegisterCustomerCommand request) => new Customer()
    {
        DepositAddress = null,
        WithdrawalAddress = request.WithdrawalAddress,
        DepositAddressDerivationPath = null,
        CreateTimestamp = _timeProvider.GetUtcNow,
        UpdateTimestamp = _timeProvider.GetUtcNow,
    };
}