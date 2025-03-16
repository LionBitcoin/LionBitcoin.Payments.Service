using System.Reflection;
using LionBitcoin.Payments.Service.Application.Services;
using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using LionBitcoin.Payments.Service.Application.Shared;
using LionBitcoin.Payments.Service.Common.Misc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LionBitcoin.Payments.Service.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddSingleton<IWalletService, HdWalletService>();
        services.AddSingleton<ITimeProviderService, LionBitcoinTimeProvider>();
        
        services.GetAndConfigure<PaymentServiceSettings>("PaymentServiceSettings");
        return services;
    }
}