using LionBitcoin.Payments.Service.Application;
using LionBitcoin.Payments.Service.Application.Clients;
using LionBitcoin.Payments.Service.Application.Services;
using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using LionBitcoin.Payments.Service.Application.Shared.Settings;
using LionBitcoin.Payments.Service.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LionBitcoin.Payments.Service.Infrastructure;

namespace LionBitcoin.Payments.Service;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PaymentsProcessorSettings>(configuration.GetSection(nameof(PaymentsProcessorSettings)));
    
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining<Reference>();
        });

        services.AddScoped<IWalletService, WalletService>();
    
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MemPoolSpaceSettings>(configuration.GetSection(nameof(MemPoolSpaceSettings)));

        services.AddSingleton<IMemPoolSpaceClient, MemPoolSpaceClient>();
    
        return services;
    }
}