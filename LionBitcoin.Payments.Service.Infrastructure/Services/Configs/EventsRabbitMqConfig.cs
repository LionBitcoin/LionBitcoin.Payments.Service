namespace LionBitcoin.Payments.Service.Infrastructure.Services.Configs;

public class EventsRabbitMqConfig
{
    public required string Host { get; set; }

    public required string VirtualHost { get; set; }

    public required string Password { get; set; }

    public required string UserName { get; set; }

    public required string Port { get; set; }
}