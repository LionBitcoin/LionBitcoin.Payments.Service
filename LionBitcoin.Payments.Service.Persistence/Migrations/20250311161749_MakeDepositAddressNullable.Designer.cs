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
    [Migration("20250311161749_MakeDepositAddressNullable")]
    partial class MakeDepositAddressNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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
                        .IsRequired()
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

                    b.ToTable("customers", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
