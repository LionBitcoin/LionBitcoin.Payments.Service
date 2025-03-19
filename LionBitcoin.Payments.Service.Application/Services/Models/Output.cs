namespace LionBitcoin.Payments.Service.Application.Services.Models;

public class Output
{
    public string Address { get; set; }

    /// <summary>
    /// Amount is in satoshis
    /// </summary>
    public long Amount { get; set; }
}