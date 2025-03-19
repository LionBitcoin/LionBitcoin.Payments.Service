using LionBitcoin.Payments.Service.Application.Domain.Events.Base;

namespace LionBitcoin.Payments.Service.Application.Domain.Events;

public class BitcoinTransactionOccured : BaseEvent
{
    public string TransactionId { get; set; }
}