using System.ComponentModel.DataAnnotations;

namespace LionBitcoin.Payments.Service.Infrastructure.Services.Configs;

public class EventsDbConfig
{
    [Required]
    public string Host { get; set; }

    [Required]
    public bool Pooling { get; set; }

    [Required]
    public int MaxPoolSize { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public int Port { get; set; }

    [Required]
    public string Database { get; set; }
}