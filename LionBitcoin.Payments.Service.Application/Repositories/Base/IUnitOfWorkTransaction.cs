using System;
using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Domain.Events.Base;

namespace LionBitcoin.Payments.Service.Application.Repositories.Base;

public interface IUnitOfWorkTransaction : IDisposable
{
    /// <summary>
    /// Submit the transaction context of the CAP, we will send the message to the message queue at the time of submission
    /// </summary>
    void Commit();

    /// <summary>
    /// Submit the transaction context of the CAP, we will send the message to the message queue at the time of submission
    /// </summary>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// We will delete the message data that has not been store in the buffer data of current transaction context.
    /// </summary>
    void Rollback();

    /// <summary>
    /// We will delete the message data that has not been store in the buffer data of current transaction context.
    /// </summary>
    Task RollbackAsync(CancellationToken cancellationToken = default);

    Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : BaseEvent;
}