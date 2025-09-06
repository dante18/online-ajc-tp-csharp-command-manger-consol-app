using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpCommandManagerDbContext.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commande_Adresse_AdresseId",
                table: "Commande");

            migrationBuilder.DropForeignKey(
                name: "FK_Commande_Client_ClientId",
                table: "Commande");

            migrationBuilder.DropForeignKey(
                name: "FK_ProduitCommande_Commande_CommandeId",
                table: "ProduitCommande");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Commande",
                table: "Commande");

            migrationBuilder.RenameTable(
                name: "Commande",
                newName: "Commandes");

            migrationBuilder.RenameIndex(
                name: "IX_Commande_ClientId",
                table: "Commandes",
                newName: "IX_Commandes_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Commande_AdresseId",
                table: "Commandes",
                newName: "IX_Commandes_AdresseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Commandes",
                table: "Commandes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Adresse_AdresseId",
                        column: x => x.AdresseId,
                        principalTable: "Adresse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_AdresseId",
                table: "Utilisateurs",
                column: "AdresseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Adresse_AdresseId",
                table: "Commandes",
                column: "AdresseId",
                principalTable: "Adresse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Utilisateurs_ClientId",
                table: "Commandes",
                column: "ClientId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ProduitCommande_Commandes_CommandeId",
                table: "ProduitCommande",
                column: "CommandeId",
                principalTable: "Commandes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Adresse_AdresseId",
                table: "Commandes");

            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Utilisateurs_ClientId",
                table: "Commandes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProduitCommande_Commandes_CommandeId",
                table: "ProduitCommande");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Commandes",
                table: "Commandes");

            migrationBuilder.RenameTable(
                name: "Commandes",
                newName: "Commande");

            migrationBuilder.RenameIndex(
                name: "IX_Commandes_ClientId",
                table: "Commande",
                newName: "IX_Commande_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Commandes_AdresseId",
                table: "Commande",
                newName: "IX_Commande_AdresseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Commande",
                table: "Commande",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdresseId = table.Column<int>(type: "int", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_Adresse_AdresseId",
                        column: x => x.AdresseId,
                        principalTable: "Adresse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_AdresseId",
                table: "Client",
                column: "AdresseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commande_Adresse_AdresseId",
                table: "Commande",
                column: "AdresseId",
                principalTable: "Adresse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commande_Client_ClientId",
                table: "Commande",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProduitCommande_Commande_CommandeId",
                table: "ProduitCommande",
                column: "CommandeId",
                principalTable: "Commande",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
