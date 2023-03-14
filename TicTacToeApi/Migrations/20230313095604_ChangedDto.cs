using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicTacToeApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tables_GameId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "SizeX",
                table: "Tables");

            migrationBuilder.RenameColumn(
                name: "SizeY",
                table: "Tables",
                newName: "Size");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_GameId",
                table: "Tables",
                column: "GameId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tables_GameId",
                table: "Tables");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Tables",
                newName: "SizeY");

            migrationBuilder.AddColumn<int>(
                name: "SizeX",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_GameId",
                table: "Tables",
                column: "GameId");
        }
    }
}
