namespace LionBitcoin.Payments.Service.Application.Domain.Events.Base;

public class BaseEvent
{
    public required string OriginalProducer { get; set; }
}