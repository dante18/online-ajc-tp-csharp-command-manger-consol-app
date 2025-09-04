using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpCommandManagerDbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenProduitAndCommande : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProduitCommande_ProduitId",
                table: "ProduitCommande",
                column: "ProduitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProduitCommande_Produit_ProduitId",
                table: "ProduitCommande",
                column: "ProduitId",
                principalTable: "Produit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProduitCommande_Produit_ProduitId",
                table: "ProduitCommande");

            migrationBuilder.DropIndex(
                name: "IX_ProduitCommande_ProduitId",
                table: "ProduitCommande");
        }
    }
}
