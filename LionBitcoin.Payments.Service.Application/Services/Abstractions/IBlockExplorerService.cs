using System.Threading;
using System.Threading.Tasks;

namespace LionBitcoin.Payments.Service.Application.Services.Abstractions;

public interface IBlockExplorerService
{
    Task<string[]> GetBlockTransactionsIds(long blockHeight, CancellationToken cancellationToken = default);
}