using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineOrderProcessing.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "BADC4D63-A4FB-438A-B29F-8B6421AD6D8D", "BADC4D63-A4FB-438A-B29F-8B6421AD6D8D", "Admin", "ADMIN" },
                    { "WQDC4D63-A4FB-438A-B29F-8B6421AD6D8D", "1FFCE66A-E440-43CE-A0CC-A4B75E238A01", "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "BADC4D63-A4FB-438A-B29F-8B6421AD6D8D");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "WQDC4D63-A4FB-438A-B29F-8B6421AD6D8D");
        }
    }
}
