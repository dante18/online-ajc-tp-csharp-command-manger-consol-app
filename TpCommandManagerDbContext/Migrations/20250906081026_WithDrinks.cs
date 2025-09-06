using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpCommandManagerDbContext.Migrations
{
    /// <inheritdoc />
    public partial class WithDrinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boisson_Produit_Id",
                table: "Boisson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boisson",
                table: "Boisson");

            migrationBuilder.RenameTable(
                name: "Boisson",
                newName: "Boissons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boissons",
                table: "Boissons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boissons_Produit_Id",
                table: "Boissons",
                column: "Id",
                principalTable: "Produit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boissons_Produit_Id",
                table: "Boissons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boissons",
                table: "Boissons");

            migrationBuilder.RenameTable(
                name: "Boissons",
                newName: "Boisson");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boisson",
                table: "Boisson",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boisson_Produit_Id",
                table: "Boisson",
                column: "Id",
                principalTable: "Produit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
