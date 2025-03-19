using DotNetCore.CAP;
using LionBitcoin.Payments.Service.Application.Domain.Events.Base;
using MediatR;

namespace LionBitcoin.Payments.Service.Infrastructure.Consumers.Base;

public interface ILionBitcoinSubscriber<in TEvent> : ICapSubscribe, IRequestHandler<TEvent>
    where TEvent : BaseEvent
{
}