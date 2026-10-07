using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineOrderProcessing.Migrations
{
    /// <inheritdoc />
    public partial class ConcurrencySafety : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderRequestKey",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Products_Stock_NonNegative",
                table: "Products",
                sql: "[Stock] >= 0");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderRequestKey",
                table: "Orders",
                column: "OrderRequestKey",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Products_Stock_NonNegative",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderRequestKey",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "OrderRequestKey",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
