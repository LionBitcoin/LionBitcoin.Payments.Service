using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Domain.Entities.Base;

namespace LionBitcoin.Payments.Service.Application.Repositories.Base;

public interface IBaseRepository<TEntity, TIdentifier>
    where TIdentifier : struct
    where TEntity : BaseEntity<TIdentifier>
{
    Task<TIdentifier> Insert(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns null if entity with specified identifier not found
    /// </summary>
    Task<TEntity?> GetById(TIdentifier identifier, bool @lock, CancellationToken cancellationToken = default);

    Task Update(TEntity entity, CancellationToken cancellationToken = default);

    Task Delete(TEntity entity, CancellationToken cancellationToken = default);
}