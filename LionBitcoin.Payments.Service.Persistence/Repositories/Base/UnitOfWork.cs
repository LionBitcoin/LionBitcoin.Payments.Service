using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using DotNetCore.CAP;
using LionBitcoin.Payments.Service.Application.Domain.Events.Base;
using LionBitcoin.Payments.Service.Application.Repositories.Base;
using LionBitcoin.Payments.Service.Persistence.Repositories.Configs;
using Microsoft.EntityFrameworkCore;

namespace LionBitcoin.Payments.Service.Persistence.Repositories.Base;

public class UnitOfWork<TDbContext> : IUnitOfWork
    where TDbContext : DbContext
{
    private readonly DbContext _dbContext;
    private readonly ICapPublisher _capPublisher;

    public UnitOfWork(TDbContext dbContext, ICapPublisher capPublisher)
    {
        _dbContext = dbContext;
        _capPublisher = capPublisher;
    }

    public Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        IDbConnection connection = _dbContext.Database.GetDbConnection();
        ICapTransaction transaction = connection.BeginTransaction(
            isolationLevel: IsolationLevel.ReadCommitted,
            publisher: _capPublisher, 
            autoCommit: false);
        return Task.FromResult(EnsureTransactionIsNotNullAndReturn(transaction));
    }

    private static IDbTransaction EnsureTransactionIsNotNullAndReturn(ICapTransaction transaction)
    {
        if (transaction.DbTransaction == null) throw new InvalidOperationException("Transaction was not created");
        return (IDbTransaction)transaction.DbTransaction;
    }

    public Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : BaseEvent
    {
        EventsCache<TEvent> eventMetadata = EventsCache<TEvent>.GetCachedMetadata(); 
        return _capPublisher.PublishAsync(eventMetadata.EventName, @event, cancellationToken: cancellationToken);
    }
}