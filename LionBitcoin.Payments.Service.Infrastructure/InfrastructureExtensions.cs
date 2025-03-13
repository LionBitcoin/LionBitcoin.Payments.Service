using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using LionBitcoin.Payments.Service.Infrastructure.Services;
using LionBitcoin.Payments.Service.Infrastructure.Services.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace LionBitcoin.Payments.Service.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCap(configuration);
        services.AddScoped<IEventsService, EventsService>();

        return services;
    }

    private static IServiceCollection AddCap(this IServiceCollection services, IConfiguration configuration)
    {
        EventsRabbitMqConfig eventsRabbitMqConfig = ConfigureAndGetEventsRabbitMqConfig(services, configuration);

        string connectionString = RegisterAndGetConnectionString(services, configuration);
        services.AddCap(options =>
        {
            options.UsePostgreSql(connectionString);
            options.UseRabbitMQ(rabbitOptions =>
            {
                rabbitOptions.HostName = eventsRabbitMqConfig.Host;
                rabbitOptions.VirtualHost = eventsRabbitMqConfig.VirtualHost;
                rabbitOptions.Port = eventsRabbitMqConfig.Port;
                rabbitOptions.UserName = eventsRabbitMqConfig.UserName;
                rabbitOptions.Password = eventsRabbitMqConfig.Password;
            });
        });

        return services;
    }

    private static EventsRabbitMqConfig ConfigureAndGetEventsRabbitMqConfig(IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection section = configuration.GetSection("EventsRabbitMqSettings");
        services.AddOptions<EventsRabbitMqConfig>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        EventsRabbitMqConfig config = new EventsRabbitMqConfig();
        section.Bind(config);
        return config;
    }

    private static string RegisterAndGetConnectionString(IServiceCollection services, IConfiguration configs)
    {
        EventsDbConfig config = RegisterAndGetEventsDatabaseConfig(services, configs);

        NpgsqlConnectionStringBuilder connectionStringBuilder = new NpgsqlConnectionStringBuilder()
        {
            Host = config.Host,
            Pooling = config.Pooling,
            MaxPoolSize = config.MaxPoolSize,
            Username = config.Username,
            Password = config.Password,
            Port = config.Port,
            Database = config.Database,
        };
        return connectionStringBuilder.ConnectionString;
    }

    private static EventsDbConfig RegisterAndGetEventsDatabaseConfig(IServiceCollection services, IConfiguration configs)
    {
        IConfigurationSection section = configs.GetSection("PaymentsServiceDb");
        services.AddOptions<EventsDbConfig>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        EventsDbConfig config = new EventsDbConfig();
        section.Bind(config);
        return config;
    }
}