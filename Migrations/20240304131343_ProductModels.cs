using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fruit_market_api.Migrations
{
    /// <inheritdoc />
    public partial class ProductModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "product",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "product",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "product");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "product");
        }
    }
}
