using System.Threading.Tasks;
using DotNetCore.CAP;
using LionBitcoin.Payments.Service.Application.Domain.Events.Base;
using LionBitcoin.Payments.Service.Application.Repositories;

namespace LionBitcoin.Payments.Service.Persistence.Repositories;

public class EventsRepository : IEventsRepository
{
    internal readonly ICapPublisher CapPublisher;

    public EventsRepository(ICapPublisher capPublisher)
    {
        CapPublisher = capPublisher;
    }

    public Task Publish<TEvent>() where TEvent : BaseEvent
    {
        throw new System.NotImplementedException();
    }
}