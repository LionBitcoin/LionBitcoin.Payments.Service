using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Domain.Entities;
using LionBitcoin.Payments.Service.Application.Repositories.Base;

namespace LionBitcoin.Payments.Service.Application.Repositories;

public interface ICustomerRepository : IBaseRepository<Customer, int>
{
    /// <summary>
    /// Returns null if address was not found ind database
    /// </summary>
    Task<Customer?> GetCustomerByDepositAddress(string address, CancellationToken cancellationToken = default);
}