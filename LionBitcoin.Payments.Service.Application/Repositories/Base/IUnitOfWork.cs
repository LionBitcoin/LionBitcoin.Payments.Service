using System.Data;
using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Domain.Events.Base;

namespace LionBitcoin.Payments.Service.Application.Repositories.Base;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : BaseEvent;
}