using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Domain.Events.Base;

namespace LionBitcoin.Payments.Service.Application.Repositories;

public interface IEventsRepository
{
    Task Publish<TEvent>() where TEvent : BaseEvent;
}