using LionBitcoin.Payments.Service.Application.Services.Abstractions;

namespace LionBitcoin.Payments.Service.Infrastructure.Services;

public class BlockExplorerService : IBlockExplorerService
{
    public Task<string> GetBlockHash(long blockHeight, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}