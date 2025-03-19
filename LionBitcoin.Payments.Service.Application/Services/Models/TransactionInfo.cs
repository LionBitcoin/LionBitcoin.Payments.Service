using System.Collections.Generic;

namespace LionBitcoin.Payments.Service.Application.Services.Models;

public class TransactionInfo
{
    public IEnumerable<Output> Outputs { get; set; }
}