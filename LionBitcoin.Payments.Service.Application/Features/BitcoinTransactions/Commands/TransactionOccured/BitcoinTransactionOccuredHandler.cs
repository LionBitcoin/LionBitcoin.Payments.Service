using System;
using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LionBitcoin.Payments.Service.Application.Features.BitcoinTransactions.Commands.TransactionOccured;

public class BitcoinTransactionOccuredHandler : IRequestHandler<BitcoinTransactionOccured>
{
    private readonly ILogger<BitcoinTransactionOccuredHandler> _logger;

    public BitcoinTransactionOccuredHandler(ILogger<BitcoinTransactionOccuredHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(BitcoinTransactionOccured request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.TransactionId);
        throw new Exception();
    }
}