using LionBitcoin.Payments.Service.Application.Domain.Entities;

namespace LionBitcoin.Payments.Service.Application.Features.Customers.Commands.RegisterCustomer;

public class RegisterCustomerResponse
{
    public required Customer Customer { get; set; }
}