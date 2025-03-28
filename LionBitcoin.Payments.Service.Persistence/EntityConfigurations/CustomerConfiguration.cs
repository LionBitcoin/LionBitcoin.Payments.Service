using LionBitcoin.Payments.Service.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LionBitcoin.Payments.Service.Persistence.EntityConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(customer => customer.Id);

        builder.HasIndex(customer => customer.DepositAddress)
            .IsUnique();

        builder.Property(customer => customer.DepositAddress)
            .IsRequired(false)
            .HasMaxLength(256);

        builder.Property(customer => customer.WithdrawalAddress)
            .IsRequired(false)
            .HasMaxLength(256);

        builder.Property(customer => customer.DepositAddressDerivationPath)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(customer => customer.CreateTimestamp)
            .IsRequired(true);

        builder.Property(customer => customer.UpdateTimestamp)
            .IsRequired(true);

        builder.Property(customer => customer.Balance)
            .IsRequired(true);
    }
}