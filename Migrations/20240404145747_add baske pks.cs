using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fruit_market_api.Migrations
{
    /// <inheritdoc />
    public partial class addbaskepks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_basket",
                table: "basket");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "basket",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_basket",
                table: "basket",
                columns: new[] { "UserId", "ProductId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_basket",
                table: "basket");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "basket",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_basket",
                table: "basket",
                column: "UserId");
        }
    }
}
