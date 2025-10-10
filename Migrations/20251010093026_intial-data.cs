using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KaShop1.Migrations
{
    /// <inheritdoc />
    public partial class intialdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "catgeries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mobiles" },
                    { 2, "tablet" },
                    { 3, "Laptop" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "catgeries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "catgeries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "catgeries",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
