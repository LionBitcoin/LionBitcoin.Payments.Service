using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LionBitcoin.Payments.Service.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingIndexOnDepositAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_customers_deposit_address",
                table: "customers",
                column: "deposit_address",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_customers_deposit_address",
                table: "customers");
        }
    }
}
