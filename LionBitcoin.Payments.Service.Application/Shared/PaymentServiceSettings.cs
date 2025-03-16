using System.ComponentModel.DataAnnotations;

namespace LionBitcoin.Payments.Service.Application.Shared;

public class PaymentServiceSettings
{
    [Required]
    public Network Network { get; set; }

    [Required]
    public string AccountExtendedPublicKey { get; set; }

    [Required]
    public long ThresholdBlockIndex { get; set; }
}