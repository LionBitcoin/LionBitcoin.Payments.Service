using DotNetCore.CAP;
using LionBitcoin.Payments.Service.Application.Domain.Events.Base;

namespace LionBitcoin.Payments.Service.Infrastructure.Consumers.Base;

public class LionBitcoinSubscriberAttribute<TEvent> : CapSubscribeAttribute
    where TEvent : BaseEvent
{
    public LionBitcoinSubscriberAttribute() : base(typeof(TEvent).Name) { }
}