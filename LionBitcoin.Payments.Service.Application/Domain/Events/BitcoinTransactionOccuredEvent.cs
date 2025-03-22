using LionBitcoin.Payments.Service.Application.Domain.Events.Base;

namespace LionBitcoin.Payments.Service.Application.Domain.Events;

public class BitcoinTransactionOccuredEvent : BaseEvent
{
    public string TransactionId { get; set; }
}