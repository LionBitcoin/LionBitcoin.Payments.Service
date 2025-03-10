using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Domain.Entities.Base;
using LionBitcoin.Payments.Service.Application.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace LionBitcoin.Payments.Service.Persistence.Repositories.Base;

public abstract class BaseRepository<TEntity, TIdentifier> : IBaseRepository<TEntity, TIdentifier>
    where TIdentifier : struct
    where TEntity : BaseEntity<TIdentifier>
{
    private readonly PaymentsServiceDbContext _dbContext;

    public BaseRepository(PaymentsServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TIdentifier> Insert(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<TEntity>().Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    /// <summary>
    /// Returns null if entity with specified identifier not found
    /// </summary>
    public Task<TEntity?> GetById(TIdentifier identifier, bool @lock, CancellationToken cancellationToken = default)
    {
        return _dbContext.Set<TEntity>()
            .SingleOrDefaultAsync(
                entity => entity.Id.Equals(identifier), 
                cancellationToken);
    }

    public async Task Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}