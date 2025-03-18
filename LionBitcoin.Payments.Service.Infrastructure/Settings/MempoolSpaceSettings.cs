using System.ComponentModel.DataAnnotations;

namespace LionBitcoin.Payments.Service.Infrastructure.Settings;

public class MempoolSpaceSettings
{
    [Required]
    public string BaseAddress { get; set; }

    [Required]
    public string ClientName { get; set; }
}