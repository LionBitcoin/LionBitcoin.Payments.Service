using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LionBitcoin.Payments.Service.Common.Misc;

public static class DependencyInjectionExtensions
{
    private static IConfiguration? _config;

    public static TSettings GetAndConfigure<TSettings>(this IServiceCollection services, string configPath)
        where TSettings : class, new()
    {
        TryExtractConfiguration(services);

        services.AddOptions<TSettings>()
            .BindConfiguration(configPath)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        TSettings setting = new TSettings();
        _config!.GetSection(configPath).Bind(setting);

        return setting;
    }

    private static void TryExtractConfiguration(IServiceCollection services)
    {
        if (_config == null)
        {
            _config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        }
    }
}