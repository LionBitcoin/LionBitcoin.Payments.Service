using LionBitcoin.Payments.Service.Application.Domain.Entities;
using LionBitcoin.Payments.Service.Application.Repositories;
using LionBitcoin.Payments.Service.Application.Repositories.Base;
using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using LionBitcoin.Payments.Service.Application.Shared;
using LionBitcoin.Payments.Service.Persistence.Repositories;
using LionBitcoin.Payments.Service.Persistence.Repositories.Base;
using LionBitcoin.Payments.Service.Persistence.Repositories.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using LionBitcoin.Payments.Service.Common.Misc;
using Microsoft.Extensions.Options;

namespace LionBitcoin.Payments.Service.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configs)
    {
        services.ConfigureDatabase(configs);
        services.AddScoped<IUnitOfWork, UnitOfWork<PaymentsServiceDbContext>>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IBlockExplorerMetadataRepository, BlockExplorerMetadataRepository>();
        services.AddCap(configs);

        return services;
    }

    public static IHost UsePersistence(this IHost host)
    {
        using IServiceScope scope = host.Services.CreateScope();
        DbContext context = scope.ServiceProvider.GetRequiredService<PaymentsServiceDbContext>();
        context.Database.Migrate();
        SeedData(scope);
        return host;
    }

    private static void SeedData(IServiceScope scope)
    {
        PaymentServiceSettings settings = scope.ServiceProvider.GetRequiredService<IOptions<PaymentServiceSettings>>().Value;
        IBlockExplorerMetadataRepository metadataRepository =
            scope.ServiceProvider.GetRequiredService<IBlockExplorerMetadataRepository>();
        ITimeProviderService timeProviderService = scope.ServiceProvider.GetRequiredService<ITimeProviderService>();
        SeedBlockExplorerMetadata(metadataRepository, settings, timeProviderService);
    }

    private static void SeedBlockExplorerMetadata(IBlockExplorerMetadataRepository metadataRepository,
        PaymentServiceSettings settings, ITimeProviderService timeProviderService)
    {
        int? thresholdBlockIndexMetadataId = metadataRepository.CreateIfNotExists(new BlockExplorerMetadata()
        {
            Key = "BlockIndexThreshold",
            Value = settings.DefaultThresholdBlockIndex.ToString(),
            CreateTimestamp = timeProviderService.GetUtcNow,
            UpdateTimestamp = timeProviderService.GetUtcNow,
        }).Result;
    }

    private static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configs)
    {
        string connectionString = GetConnectionString(configs);
        services.AddDbContext<PaymentsServiceDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseSnakeCaseNamingConvention();
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        return services;
    }

    private static string GetConnectionString(IConfiguration configs)
    {
        NpgsqlConnectionStringBuilder connectionStringBuilder = new NpgsqlConnectionStringBuilder();
        configs.GetSection("PaymentsServiceDb").Bind(connectionStringBuilder);
        return connectionStringBuilder.ConnectionString;
    }

    private static IServiceCollection AddCap(this IServiceCollection services, IConfiguration configuration)
    {
        EventsRabbitMqConfig eventsRabbitMqConfig =
            services.GetAndConfigure<EventsRabbitMqConfig>("EventsRabbitMqSettings");

        string connectionString = GetConnectionString(configuration);
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
}