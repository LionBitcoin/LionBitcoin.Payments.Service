using LionBitcoin.Payments.Service.Application.Domain.Entities;
using LionBitcoin.Payments.Service.Application.Repositories;
using LionBitcoin.Payments.Service.Persistence.Repositories.Base;

namespace LionBitcoin.Payments.Service.Persistence.Repositories;

public class CustomerRepository : BaseRepository<Customer,int>, ICustomerRepository
{
    public CustomerRepository(PaymentsServiceDbContext dbContext) : base(dbContext)
    {
    }
}