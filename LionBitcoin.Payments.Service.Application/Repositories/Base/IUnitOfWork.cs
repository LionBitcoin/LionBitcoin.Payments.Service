using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace LionBitcoin.Payments.Service.Application.Repositories.Base;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}