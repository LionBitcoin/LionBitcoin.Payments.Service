using LionBitcoin.Payments.Service.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LionBitcoin.Payments.Service.Persistence.EntityConfigurations;

public class BlockExplorerMetadataConfiguration : IEntityTypeConfiguration<BlockExplorerMetadata>
{
    public void Configure(EntityTypeBuilder<BlockExplorerMetadata> builder)
    {
        builder.HasKey(metadata => metadata.Id);

        builder.HasIndex(metadata => metadata.Key)
            .IsUnique();

        builder.Property(metadata => metadata.Key)
            .IsRequired(true)
            .HasMaxLength(256);

        builder.Property(metadata => metadata.Value)
            .IsRequired(true)
            .HasMaxLength(500);

        builder.Property(metadata => metadata.CreateTimestamp)
            .IsRequired(true);

        builder.Property(metadata => metadata.UpdateTimestamp)
            .IsRequired(true);

    }
}