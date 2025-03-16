using System.Threading;
using System.Threading.Tasks;

namespace LionBitcoin.Payments.Service.Application.Repositories.Base;

public interface IUnitOfWork
{
    Task<IUnitOfWorkTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}