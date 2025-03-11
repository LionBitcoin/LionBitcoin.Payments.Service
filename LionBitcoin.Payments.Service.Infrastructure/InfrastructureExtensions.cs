using LionBitcoin.Payments.Service.Infrastructure.Services.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace LionBitcoin.Payments.Service.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        
        return services;
    }

    private static IServiceCollection AddCap(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EventsRabbitMqConfig>(configuration.GetSection("EventsRabbitMqSettings"));
        string connectionString = GetConnectionString(configuration);
        services.AddCap(options =>
        {
            options.UsePostgreSql(connectionString);
            options.UseRabbitMQ(rabbitOptions =>
            {
                rabbitOptions.HostName = "";
                rabbitOptions.VirtualHost = "";
                rabbitOptions.Port = 12;
                rabbitOptions.UserName = "";
                rabbitOptions.Password = "";
            });
        });

        return services;
    }

    private static string GetConnectionString(IConfiguration configs)
    {
        NpgsqlConnectionStringBuilder connectionStringBuilder = new NpgsqlConnectionStringBuilder();
        configs.GetSection("PaymentsServiceDb").Bind(connectionStringBuilder);
        return connectionStringBuilder.ConnectionString;
    }
}