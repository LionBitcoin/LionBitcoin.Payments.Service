using LionBitcoin.Payments.Service.Application.Domain.Entities.Base;

namespace LionBitcoin.Payments.Service.Application.Domain.Entities;

public class Customer : BaseEntity<int>
{
    public string? DepositAddress { get; set; }

    public string? DepositAddressDerivationPath { get; set; }

    public string? WithdrawalAddress { get; set; }
}