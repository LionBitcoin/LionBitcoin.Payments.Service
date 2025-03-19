using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Domain.Entities;
using LionBitcoin.Payments.Service.Application.Domain.Events;
using LionBitcoin.Payments.Service.Application.Repositories;
using LionBitcoin.Payments.Service.Application.Repositories.Base;
using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using LionBitcoin.Payments.Service.Application.Services.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LionBitcoin.Payments.Service.Application.Features.BitcoinTransactions.Commands.TransactionOccured;

public class BitcoinTransactionOccuredHandler : IRequestHandler<BitcoinTransactionOccured>
{
    private readonly ILogger<BitcoinTransactionOccuredHandler> _logger;
    private readonly IBlockExplorerService _blockExplorerService;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BitcoinTransactionOccuredHandler(
        ILogger<BitcoinTransactionOccuredHandler> logger,
        IBlockExplorerService blockExplorerService,
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _blockExplorerService = blockExplorerService;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(BitcoinTransactionOccured request, CancellationToken cancellationToken)
    {
        TransactionInfo transactionInfo = 
            await _blockExplorerService.GetTransactionInfo(request.TransactionId, cancellationToken);

        using IUnitOfWorkTransaction transaction  = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        foreach (Output output in transactionInfo.Outputs)
        {
            await TryUpdateCustomersBalance(cancellationToken, output);
        }

        await transaction.CommitAsync(cancellationToken);
    }

    private async Task TryUpdateCustomersBalance(CancellationToken cancellationToken, Output output)
    {
        Customer? customer = await _customerRepository.GetCustomerByDepositAddress(output.Address, cancellationToken);
        if (customer != null)
        {
            customer.Balance += output.Amount;
            await _customerRepository.Update(customer, cancellationToken);
            _logger.LogInformation($"Updated account balance for {output.Address}. Added amount: {output.Amount}.");
        }
    }
}