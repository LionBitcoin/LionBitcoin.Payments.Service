using System.Threading;
using System.Threading.Tasks;
using Dapper;
using LionBitcoin.Payments.Service.Application.Domain.Entities;
using LionBitcoin.Payments.Service.Application.Repositories;
using LionBitcoin.Payments.Service.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LionBitcoin.Payments.Service.Persistence.Repositories;

public class BlockExplorerMetadataRepository
    : BaseRepository<BlockExplorerMetadata, int>, IBlockExplorerMetadataRepository
{
    private readonly PaymentsServiceDbContext _dbContext;

    public BlockExplorerMetadataRepository(PaymentsServiceDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int?> CreateOrUpdateMetadata(BlockExplorerMetadata metadata, CancellationToken cancellationToken = default)
    {
        const string query = $@"INSERT INTO block_explorer_metadata
                                (
                                    key, 
                                    value, 
                                    create_timestamp, 
                                    update_timestamp
                                )
                                VALUES 
                                (
                                    @{nameof(BlockExplorerMetadata.Key)},
                                    @{nameof(BlockExplorerMetadata.Value)},
                                    @{nameof(BlockExplorerMetadata.CreateTimestamp)},
                                    @{nameof(BlockExplorerMetadata.UpdateTimestamp)}
                                )
                                ON CONFLICT (key)
                                DO UPDATE SET 
                                    value = @{nameof(BlockExplorerMetadata.Value)},
                                    update_timestamp = @{nameof(BlockExplorerMetadata.UpdateTimestamp)}
                                RETURNING id;";
        return await _dbContext.Database.GetDbConnection()
            .QuerySingleOrDefaultAsync<int>(
                sql: query, 
                param: metadata, 
                transaction: _dbContext.Database.CurrentTransaction?.GetDbTransaction());
    }

    public async Task<int?> CreateIfNotExists(BlockExplorerMetadata metadata, CancellationToken cancellationToken = default)
    {
        const string query = $@"INSERT INTO block_explorer_metadata
                                (
                                    key, 
                                    value, 
                                    create_timestamp, 
                                    update_timestamp
                                )
                                VALUES 
                                (
                                    @{nameof(BlockExplorerMetadata.Key)},
                                    @{nameof(BlockExplorerMetadata.Value)},
                                    @{nameof(BlockExplorerMetadata.CreateTimestamp)},
                                    @{nameof(BlockExplorerMetadata.UpdateTimestamp)}
                                )
                                ON CONFLICT (key)
                                DO NOTHING
                                RETURNING id;";
        return await _dbContext.Database.GetDbConnection()
            .QuerySingleOrDefaultAsync<int>(
                sql: query, 
                param: metadata, 
                transaction: _dbContext.Database.CurrentTransaction?.GetDbTransaction());
    }
}