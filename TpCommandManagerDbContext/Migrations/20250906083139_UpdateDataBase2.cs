using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpCommandManagerDbContext.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataBase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Utilisateurs_ClientId",
                table: "Commandes");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Commandes",
                newName: "UtilisateurId");

            migrationBuilder.RenameIndex(
                name: "IX_Commandes_ClientId",
                table: "Commandes",
                newName: "IX_Commandes_UtilisateurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Utilisateurs_UtilisateurId",
                table: "Commandes",
                column: "UtilisateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Utilisateurs_UtilisateurId",
                table: "Commandes");

            migrationBuilder.RenameColumn(
                name: "UtilisateurId",
                table: "Commandes",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Commandes_UtilisateurId",
                table: "Commandes",
                newName: "IX_Commandes_ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Utilisateurs_ClientId",
                table: "Commandes",
                column: "ClientId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
