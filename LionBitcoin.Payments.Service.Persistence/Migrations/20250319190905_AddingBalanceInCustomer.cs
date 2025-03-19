using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LionBitcoin.Payments.Service.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingBalanceInCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "balance",
                table: "customers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "balance",
                table: "customers");
        }
    }
}
