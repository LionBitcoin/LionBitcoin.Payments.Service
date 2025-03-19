using LionBitcoin.Payments.Service.Application.Domain.Events;
using LionBitcoin.Payments.Service.Infrastructure.Consumers.Base;
using MediatR;

namespace LionBitcoin.Payments.Service.Infrastructure.Consumers;

public class TransactionsConsumer : 
    ILionBitcoinSubscriber<BitcoinTransactionOccured>
{
    private readonly IMediator _mediator;

    public TransactionsConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    [LionBitcoinSubscriber<BitcoinTransactionOccured>]
    public async Task Handle(BitcoinTransactionOccured @event)
    {
        await _mediator.Send(@event);
    }
}