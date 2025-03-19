using MediatR;

namespace LionBitcoin.Payments.Service.Application.Domain.Events.Base;

public abstract class BaseEvent : IRequest
{
    public required string OriginalProducer { get; set; }
}