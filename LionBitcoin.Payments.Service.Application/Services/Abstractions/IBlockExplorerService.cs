using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Services.Models;

namespace LionBitcoin.Payments.Service.Application.Services.Abstractions;

public interface IBlockExplorerService
{
    Task<string[]> GetBlockTransactionsIds(long blockHeight, CancellationToken cancellationToken = default);

    Task<TransactionInfo> GetTransactionInfo(string transactionId, CancellationToken cancellationToken = default);
}