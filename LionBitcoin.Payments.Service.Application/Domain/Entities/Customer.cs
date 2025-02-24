using System;
using LionBitcoin.Payments.Service.Application.Domain.Entities.Base;

namespace LionBitcoin.Payments.Service.Application.Domain.Entities;

public class Customer : BaseEntity<Guid>
{
    public string DepositAddress { get; set; }

    public string WithdrawalAddress { get; set; }
}