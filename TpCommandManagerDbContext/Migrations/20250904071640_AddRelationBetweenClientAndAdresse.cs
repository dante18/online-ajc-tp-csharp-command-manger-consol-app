using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpCommandManagerDbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenClientAndAdresse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdresseId",
                table: "Client",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Client_AdresseId",
                table: "Client",
                column: "AdresseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Adresse_AdresseId",
                table: "Client",
                column: "AdresseId",
                principalTable: "Adresse",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Adresse_AdresseId",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_AdresseId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "AdresseId",
                table: "Client");
        }
    }
}
