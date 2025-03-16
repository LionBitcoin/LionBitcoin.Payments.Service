using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LionBitcoin.Payments.Service.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MakeMetadataKeyUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_block_explorer_metadata_key",
                table: "block_explorer_metadata");

            migrationBuilder.CreateIndex(
                name: "ix_block_explorer_metadata_key",
                table: "block_explorer_metadata",
                column: "key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_block_explorer_metadata_key",
                table: "block_explorer_metadata");

            migrationBuilder.CreateIndex(
                name: "ix_block_explorer_metadata_key",
                table: "block_explorer_metadata",
                column: "key");
        }
    }
}
