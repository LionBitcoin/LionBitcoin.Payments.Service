using System.Threading;
using System.Threading.Tasks;
using DotNetCore.CAP;
using LionBitcoin.Payments.Service.Application.Domain.Events.Base;
using LionBitcoin.Payments.Service.Application.Repositories;
using LionBitcoin.Payments.Service.Persistence.Repositories.Configs;

namespace LionBitcoin.Payments.Service.Persistence.Repositories;

public class EventsRepository : IEventsRepository
{
    internal readonly ICapPublisher CapPublisher;

    public EventsRepository(ICapPublisher capPublisher)
    {
        CapPublisher = capPublisher;
    }

    public async Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : BaseEvent
    {
        EventsCache<TEvent> eventMetadata = EventsCache<TEvent>.GetCachedMetadata(); 
        await CapPublisher.PublishAsync(eventMetadata.EventName, @event, cancellationToken: cancellationToken);
    }
}