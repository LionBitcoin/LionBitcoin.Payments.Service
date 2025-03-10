using LionBitcoin.Payments.Service.Application.Domain.Entities;
using LionBitcoin.Payments.Service.Application.Repositories.Base;

namespace LionBitcoin.Payments.Service.Application.Repositories;

public interface ICustomerRepository : IBaseRepository<Customer, int>
{
    
}