using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using LionBitcoin.Payments.Service.Application.Domain.Entities;
using LionBitcoin.Payments.Service.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LionBitcoin.Payments.Service.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly PaymentsServiceDbContext _dbContext;

    public CustomerRepository(PaymentsServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer?> GetCustomerByDepositAddress(string address, CancellationToken cancellationToken = default)
    {
        const string query = $@"SELECT
                                    id as {nameof(Customer.Id)},
                                    deposit_address as {nameof(Customer.DepositAddress)},
                                    deposit_address_derivation_path as {nameof(Customer.DepositAddressDerivationPath)},
                                    withdrawal_address as {nameof(Customer.WithdrawalAddress)},
                                    create_timestamp as {nameof(Customer.CreateTimestamp)},
                                    update_timestamp as {nameof(Customer.UpdateTimestamp)}
                                FROM
                                    customers
                                WHERE
                                    deposit_address = @{nameof(address)};";

        Customer? customer = await _dbContext.Database.GetDbConnection()
            .QuerySingleOrDefaultAsync<Customer>(
                sql: query, 
                param: new { address },
                transaction: _dbContext.Database.CurrentTransaction?.GetDbTransaction());

        return customer;

    }

    public async Task<int> Insert(Customer entity, CancellationToken cancellationToken = default)
    {
        const string query = $@"INSERT INTO customers
                            (
                                balance,
                                deposit_address,
                                deposit_address_derivation_path,
                                withdrawal_address,
                                create_timestamp,
                                update_timestamp
                            )
                            VALUES
                            (
                                @{nameof(Customer.Balance)},
                                @{nameof(Customer.DepositAddress)},
                                @{nameof(Customer.DepositAddressDerivationPath)},
                                @{nameof(Customer.WithdrawalAddress)},
                                @{nameof(Customer.CreateTimestamp)},
                                @{nameof(Customer.UpdateTimestamp)}
                            )
                            RETURNING id;";
        return await _dbContext.Database.GetDbConnection()
            .QuerySingleAsync<int>(
                sql: query, 
                param: entity,
                transaction: _dbContext.Database.CurrentTransaction?.GetDbTransaction());
    }

    public Task<Customer?> GetById(int identifier, bool @lock, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }

    public async Task Update(Customer entity, CancellationToken cancellationToken = default)
    {
        const string query = $@"UPDATE customers
                                SET
                                    balance = @{nameof(Customer.Balance)},
                                    deposit_address = @{nameof(Customer.DepositAddress)},
                                    deposit_address_derivation_path = @{nameof(Customer.DepositAddressDerivationPath)},
                                    withdrawal_address = @{nameof(Customer.WithdrawalAddress)},
                                    create_timestamp = @{nameof(Customer.CreateTimestamp)},
                                    update_timestamp = @{nameof(Customer.UpdateTimestamp)}
                                WHERE id = @{nameof(Customer.Id)};";

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

    public Task Delete(Customer entity, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }
}