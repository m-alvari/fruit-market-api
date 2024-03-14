using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fruit_market_api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRefreshTokenNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_userRefreshToken",
                table: "userRefreshToken");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "userRefreshToken",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "userRefreshToken",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_userRefreshToken",
                table: "userRefreshToken",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_userRefreshToken",
                table: "userRefreshToken");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "userRefreshToken");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "userRefreshToken",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_userRefreshToken",
                table: "userRefreshToken",
                column: "UserId");
        }
    }
}
