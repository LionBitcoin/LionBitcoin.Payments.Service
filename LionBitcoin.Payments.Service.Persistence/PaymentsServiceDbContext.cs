using System.Reflection;
using LionBitcoin.Payments.Service.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LionBitcoin.Payments.Service.Persistence;

public class PaymentsServiceDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public DbSet<BlockExplorerMetadata> BlockExplorerMetadata { get; set; }

    public PaymentsServiceDbContext(DbContextOptions<PaymentsServiceDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}