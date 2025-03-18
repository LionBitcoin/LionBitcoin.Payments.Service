using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using LionBitcoin.Payments.Service.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LionBitcoin.Payments.Service.Common.Misc;
using LionBitcoin.Payments.Service.Infrastructure.Settings;

namespace LionBitcoin.Payments.Service.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IBlockExplorerService, BlockExplorerService>();
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
}