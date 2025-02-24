using System;
using MediatR;

namespace LionBitcoin.Payments.Service.Application.Features.Customers.Commands.RegisterCustomer;

public class RegisterCustomerCommand : IRequest<RegisterCustomerResponse>
{
    public Guid CustomerId { get; set; }

    public string WithdrawalAddress { get; set; }
}