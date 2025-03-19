﻿// <auto-generated />
using System;
using LionBitcoin.Payments.Service.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LionBitcoin.Payments.Service.Persistence.Migrations
{
    [DbContext(typeof(PaymentsServiceDbContext))]
    [Migration("20250319190706_AddingIndexOnDepositAddress")]
    partial class AddingIndexOnDepositAddress
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LionBitcoin.Payments.Service.Application.Domain.Entities.BlockExplorerMetadata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateTimestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_timestamp");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("key");

                    b.Property<DateTime>("UpdateTimestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_timestamp");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_block_explorer_metadata");

                    b.HasIndex("Key")
                        .IsUnique()
                        .HasDatabaseName("ix_block_explorer_metadata_key");

                    b.ToTable("block_explorer_metadata", (string)null);
                });

            modelBuilder.Entity("LionBitcoin.Payments.Service.Application.Domain.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateTimestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_timestamp");

                    b.Property<string>("DepositAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("deposit_address");

                    b.Property<string>("DepositAddressDerivationPath")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("deposit_address_derivation_path");

                    b.Property<DateTime>("UpdateTimestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_timestamp");

                    b.Property<string>("WithdrawalAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("withdrawal_address");

                    b.HasKey("Id")
                        .HasName("pk_customers");

                    b.HasIndex("DepositAddress")
                        .IsUnique()
                        .HasDatabaseName("ix_customers_deposit_address");

                    b.ToTable("customers", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
