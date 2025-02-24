using LionBitcoin.Payments.Service.Application.Domain.Enums;
using MediatR;

namespace LionBitcoin.Payments.Service.Application.Features.Payments.Commands.Deposit;

public class DepositCommand : IRequest<DepositResponse>
{
    /// <summary>
    /// Check 'Domain.Enums.Currency' enum description, to get more information about 'Amount' field
    /// </summary>
    public ulong Amount { get; set; }

    public Currency Currency { get; set; }

}