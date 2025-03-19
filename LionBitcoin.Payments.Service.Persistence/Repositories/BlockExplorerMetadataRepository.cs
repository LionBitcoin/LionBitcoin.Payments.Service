using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using LionBitcoin.Payments.Service.Application.Domain.Entities;
using LionBitcoin.Payments.Service.Application.Repositories;
using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using LionBitcoin.Payments.Service.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LionBitcoin.Payments.Service.Persistence.Repositories;

public class BlockExplorerMetadataRepository
    : BaseRepository<BlockExplorerMetadata, int>, IBlockExplorerMetadataRepository
{
    private readonly PaymentsServiceDbContext _dbContext;
    private readonly ITimeProviderService _timeProvider;

    public BlockExplorerMetadataRepository(PaymentsServiceDbContext dbContext, ITimeProviderService timeProvider) : base(dbContext)
    {
        _dbContext = dbContext;
        _timeProvider = timeProvider;
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
            .QuerySingleOrDefaultAsync<int?>(
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
            .QuerySingleOrDefaultAsync<int?>(
                sql: query,
                param: metadata, 
                transaction: _dbContext.Database.CurrentTransaction?.GetDbTransaction());
    }

    public async Task<string?> GetMetadataByKey(string key, CancellationToken cancellationToken = default)
    {
        const string query = $@"SELECT 
                                    value
                                FROM 
                                    block_explorer_metadata
                                WHERE 
                                    key = @{nameof(key)};";
        return await _dbContext.Database.GetDbConnection()
            .QuerySingleOrDefaultAsync<string>(
                sql: query, 
                param: new { key },
                transaction: _dbContext.Database.CurrentTransaction?.GetDbTransaction());
    }

    public async Task UpdateMetadataByKey(string key, string value, CancellationToken cancellationToken = default)
    {
        DateTime currentTime = _timeProvider.GetUtcNow;
        const string query = $@"UPDATE 
                                    block_explorer_metadata
                                SET 
                                    value = @{nameof(value)},
                                    update_timestamp = @{nameof(currentTime)}
                                WHERE key = @{nameof(key)};";
        int rawCount = await _dbContext.Database.GetDbConnection()
            .ExecuteAsync(
                sql: query, 
                param: new { key, value, currentTime},
                transaction: _dbContext.Database.CurrentTransaction?.GetDbTransaction());

        if (rawCount == 0)
        {
            throw new InvalidOperationException("Specified key was not found in block_explorer_metadata");
        }
    }
}