using LionBitcoin.Payments.Service.Application.Domain.Events;
using LionBitcoin.Payments.Service.Infrastructure.Consumers.Base;

namespace LionBitcoin.Payments.Service.Infrastructure.Consumers;

public class TransactionsConsumer : 
    ILionBitcoinSubscriber<BitcoinTransactionOccured>
{
    [LionBitcoinSubscriber<BitcoinTransactionOccured>]
    public async Task Handle(BitcoinTransactionOccured request, CancellationToken cancellationToken = default)
    {
        Console.WriteLine(request.OriginalProducer);
    }
}