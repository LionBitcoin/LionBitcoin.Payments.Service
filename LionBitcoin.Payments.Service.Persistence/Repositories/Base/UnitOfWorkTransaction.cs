using System.Threading;
using System.Threading.Tasks;
using DotNetCore.CAP;
using LionBitcoin.Payments.Service.Application.Domain.Events.Base;
using LionBitcoin.Payments.Service.Application.Repositories.Base;
using LionBitcoin.Payments.Service.Persistence.Repositories.Configs;

namespace LionBitcoin.Payments.Service.Persistence.Repositories.Base;

public class UnitOfWorkTransaction : IUnitOfWorkTransaction
{
    private readonly ICapTransaction _capTransaction;
    private readonly ICapPublisher _publisher;

    public UnitOfWorkTransaction(
        ICapTransaction capTransaction,
        ICapPublisher publisher)
    {
        _capTransaction = capTransaction;
        _publisher = publisher;
    }


    public void Commit()
    {
        _capTransaction.Commit();
    }

    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
        return _capTransaction.CommitAsync(cancellationToken);
    }

    public void Rollback()
    {
        _capTransaction.Rollback();
    }

    public Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        return _capTransaction.RollbackAsync(cancellationToken);
    }

    public Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : BaseEvent
    {
        EventsCache<TEvent> eventMetadata = EventsCache<TEvent>.GetCachedMetadata(); 
        return _publisher.PublishAsync(eventMetadata.EventName, @event, cancellationToken: cancellationToken);
    }

    public void Dispose()
    {
        _capTransaction.Dispose();
    }
}