using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace LionBitcoin.Payments.Service.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configs)
    {
        services.ConfigureDatabase(configs);

        return services;
    }

    public static IHost UsePersistence(this IHost host)
    {
        using IServiceScope scope = host.Services.CreateScope();
        DbContext context = scope.ServiceProvider.GetRequiredService<PaymentsServiceDbContext>();
        context.Database.Migrate();
        return host;
    }

    private static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configs)
    {
        string connectionString = GetConnectionString(configs);
        services.AddDbContext<PaymentsServiceDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseSnakeCaseNamingConvention();
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