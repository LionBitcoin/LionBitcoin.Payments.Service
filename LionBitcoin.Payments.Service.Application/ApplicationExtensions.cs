using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace LionBitcoin.Payments.Service.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}