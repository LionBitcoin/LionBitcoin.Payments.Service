namespace LionBitcoin.Payments.Service.Application.Domain.Events.Base;

public abstract class BaseEvent
{
    public required string OriginalProducer { get; set; }
}