using System.Reflection;
using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using LionBitcoin.Payments.Service.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LionBitcoin.Payments.Service.Common.Misc;
using LionBitcoin.Payments.Service.Infrastructure.Consumers;
using LionBitcoin.Payments.Service.Infrastructure.Settings;
using Npgsql;

namespace LionBitcoin.Payments.Service.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IBlockExplorerService, BlockExplorerService>();
        services.AddCap(config);
        MempoolSpaceSettings mempoolSettings = services.GetAndConfigure<MempoolSpaceSettings>("MempoolClientSettings.Main");

        AddHttpClients(services, mempoolSettings);

        return services;
    }

    private static void AddHttpClients(IServiceCollection services, MempoolSpaceSettings mempoolSettings)
    {
        services.AddHttpClient(mempoolSettings.ClientName, client =>
        {
            client.BaseAddress = new Uri(mempoolSettings.BaseAddress);
        });
    }

    private static IServiceCollection AddCap(this IServiceCollection services, IConfiguration configuration)
    {
        EventsRabbitMqConfig eventsRabbitMqConfig =
            services.GetAndConfigure<EventsRabbitMqConfig>("EventsRabbitMqSettings");
        EventsConfig eventsConfig =
            services.GetAndConfigure<EventsConfig>("EventsConfig");

        string connectionString = GetConnectionString(configuration);

        AddConsumers(services);

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

            options.FailedRetryCount = eventsConfig.FailedRetryCount;
            options.FailedRetryInterval = eventsConfig.FailedRetryIntervalInSeconds;
            options.SucceedMessageExpiredAfter = eventsConfig.SucceedMessageExpiredAfterInSeconds;
            options.FailedMessageExpiredAfter = eventsConfig.FailedMessageExpiredAfterInSeconds;

            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            options.DefaultGroupName = currentAssembly.GetName().Name!;

            options.UseDashboard();
        });

        return services;
    }

    private static void AddConsumers(IServiceCollection services)
    {
        services.AddTransient<TransactionsConsumer>();
    }

    private static string GetConnectionString(IConfiguration configs)
    {
        NpgsqlConnectionStringBuilder connectionStringBuilder = new NpgsqlConnectionStringBuilder();
        configs.GetSection("PaymentsServiceDb").Bind(connectionStringBuilder);
        return connectionStringBuilder.ConnectionString;
    }
}