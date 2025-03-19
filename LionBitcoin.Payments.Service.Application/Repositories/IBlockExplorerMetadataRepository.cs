using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Domain.Entities;
using LionBitcoin.Payments.Service.Application.Repositories.Base;

namespace LionBitcoin.Payments.Service.Application.Repositories;

public interface IBlockExplorerMetadataRepository : IBaseRepository<BlockExplorerMetadata, int>
{
    Task<int?> CreateOrUpdateMetadata(BlockExplorerMetadata metadata, CancellationToken cancellationToken = default);

    Task<int?> CreateIfNotExists(BlockExplorerMetadata metadata, CancellationToken cancellationToken = default);

    Task<string?> GetMetadataByKey(string key, CancellationToken cancellationToken = default);

    Task UpdateMetadataByKey(string key, string value, CancellationToken cancellationToken = default);
}