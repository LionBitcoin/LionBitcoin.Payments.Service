using System.Threading;
using System.Threading.Tasks;

namespace LionBitcoin.Payments.Service.Application.Services.Abstractions;

public interface IBlockExplorerService
{
    Task<string> GetBlockHash(long blockHeight, CancellationToken cancellationToken = default);
}