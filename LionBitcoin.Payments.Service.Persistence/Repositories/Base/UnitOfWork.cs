using System.Data;
using System.Threading;
using System.Threading.Tasks;
using DotNetCore.CAP;
using LionBitcoin.Payments.Service.Application.Repositories;
using LionBitcoin.Payments.Service.Application.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LionBitcoin.Payments.Service.Persistence.Repositories.Base;

public class UnitOfWork<TDbContext> : IUnitOfWork
    where TDbContext : DbContext
{
    private readonly DbContext _dbContext;
    private readonly EventsRepository _eventsRepository;

    public UnitOfWork(TDbContext dbContext, IEventsRepository eventsRepository)
    {
        _dbContext = dbContext;
        _eventsRepository = (EventsRepository)eventsRepository;
    }

    public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync(_eventsRepository.CapPublisher, autoCommit: false, cancellationToken);
        return transaction.GetDbTransaction();
    }
}