using System.ComponentModel.DataAnnotations;

namespace LionBitcoin.Payments.Service.Infrastructure.Settings;

public class EventsRabbitMqConfig
{
    [Required]
    public string Host { get; set; }
 
    [Required]
    public string VirtualHost { get; set; }

    [Required]
    public string Password { get; set; }
 
    [Required]
    public string UserName { get; set; }
 
    [Required]
    public int Port { get; set; }
}