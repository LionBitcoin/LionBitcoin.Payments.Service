using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using LionBitcoin.Payments.Service.Application.Domain.Entities;
using LionBitcoin.Payments.Service.Application.Repositories;
using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LionBitcoin.Payments.Service.Persistence.Repositories;

public class BlockExplorerMetadataRepository
    : IBlockExplorerMetadataRepository
{
    private readonly PaymentsServiceDbContext _dbContext;
    private readonly ITimeProviderService _timeProvider;

    public BlockExplorerMetadataRepository(
        PaymentsServiceDbContext dbContext, 
        ITimeProviderService timeProvider)
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

    public async Task<int?> CreateOrUpdateOnlyIf(BlockExplorerMetadata metadata, Predicate<BlockExplorerMetadata> evaluatesTrue, CancellationToken cancellationToken = default)
    {
        BlockExplorerMetadata? existingMetadata = await GetMetadataByKey(metadata.Key, cancellationToken);
        if (existingMetadata == null) // Metadata not exists
        {
            int id = await Insert(metadata, cancellationToken);
            return id;
        }
        else if (evaluatesTrue(existingMetadata)) // Metadata exists and passed predicate evaluates true
        {
            existingMetadata.UpdateTimestamp = metadata.UpdateTimestamp;
            existingMetadata.Value = metadata.Value;
            await Update(existingMetadata, cancellationToken);
            return null;
        }
        else // Metadata exists and passed predicate evaluates false
        {
            return null;
        }
    }

    public async Task<BlockExplorerMetadata?> GetMetadataByKey(string key, CancellationToken cancellationToken = default)
    {
        const string query = $@"SELECT 
                                    id as {nameof(BlockExplorerMetadata.Id)},
                                    key as {nameof(BlockExplorerMetadata.Key)}, 
                                    value as {nameof(BlockExplorerMetadata.Value)}, 
                                    create_timestamp as {nameof(BlockExplorerMetadata.CreateTimestamp)}, 
                                    update_timestamp as {nameof(BlockExplorerMetadata.UpdateTimestamp)}
                                FROM 
                                    block_explorer_metadata
                                WHERE 
                                    key = @{nameof(key)};";
        return await _dbContext.Database.GetDbConnection()
            .QuerySingleOrDefaultAsync<BlockExplorerMetadata>(
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

    public async Task<int> Insert(BlockExplorerMetadata entity, CancellationToken cancellationToken = default)
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
                                    @{nameof(BlockExplorerMetadata.Key)}
                                    @{nameof(BlockExplorerMetadata.Value)},
                                    @{nameof(BlockExplorerMetadata.CreateTimestamp)},
                                    @{nameof(BlockExplorerMetadata.UpdateTimestamp)},
                                )
                                RETURNING id;";
        return await _dbContext.Database.GetDbConnection()
            .QuerySingleAsync<int>(
                sql: query, 
                param: entity,
                transaction: _dbContext.Database.CurrentTransaction?.GetDbTransaction());
    }

    public Task<BlockExplorerMetadata?> GetById(int identifier, bool @lock, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task Update(BlockExplorerMetadata entity, CancellationToken cancellationToken = default)
    {
        const string query = $@"UPDATE block_explorer_metadata
                                SET
                                    key = @{nameof(BlockExplorerMetadata.Key)},
                                    value = @{nameof(BlockExplorerMetadata.Value)},
                                    create_timestamp = @{nameof(BlockExplorerMetadata.CreateTimestamp)},
                                    update_timestamp = @{nameof(BlockExplorerMetadata.UpdateTimestamp)}
                                WHERE id = @{nameof(BlockExplorerMetadata.Id)};";
        
        int rawCount = await _dbContext.Database.GetDbConnection()
            .ExecuteAsync(
                sql: query, 
                param: entity,
                transaction: _dbContext.Database.CurrentTransaction?.GetDbTransaction());

        if (rawCount == 0)
        {
            throw new InvalidOperationException($"BlockExplorerMetadata was not found with specified Id: {entity.Id}");
        }
    }

    public Task Delete(BlockExplorerMetadata entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}