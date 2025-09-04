using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpCommandManagerDbContext.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Burger_BurgerId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Pizza_PizzaId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Pasta_Nourriture_Id",
                table: "Pasta");

            migrationBuilder.DropForeignKey(
                name: "FK_Pizza_Nourriture_Id",
                table: "Pizza");

            migrationBuilder.DropForeignKey(
                name: "FK_Pizza_Pate_PateId",
                table: "Pizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pizza",
                table: "Pizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pate",
                table: "Pate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pasta",
                table: "Pasta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "EstVegetarien",
                table: "Ingredient");

            migrationBuilder.RenameTable(
                name: "Pizza",
                newName: "Pizzas");

            migrationBuilder.RenameTable(
                name: "Pate",
                newName: "Pates");

            migrationBuilder.RenameTable(
                name: "Pasta",
                newName: "Pastas");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredients");

            migrationBuilder.RenameIndex(
                name: "IX_Pizza_PateId",
                table: "Pizzas",
                newName: "IX_Pizzas_PateId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_PizzaId",
                table: "Ingredients",
                newName: "IX_Ingredients_PizzaId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_BurgerId",
                table: "Ingredients",
                newName: "IX_Ingredients_BurgerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pizzas",
                table: "Pizzas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pates",
                table: "Pates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pastas",
                table: "Pastas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Burger_BurgerId",
                table: "Ingredients",
                column: "BurgerId",
                principalTable: "Burger",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Pizzas_PizzaId",
                table: "Ingredients",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pastas_Nourriture_Id",
                table: "Pastas",
                column: "Id",
                principalTable: "Nourriture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Nourriture_Id",
                table: "Pizzas",
                column: "Id",
                principalTable: "Nourriture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Pates_PateId",
                table: "Pizzas",
                column: "PateId",
                principalTable: "Pates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Burger_BurgerId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Pizzas_PizzaId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Pastas_Nourriture_Id",
                table: "Pastas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Nourriture_Id",
                table: "Pizzas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Pates_PateId",
                table: "Pizzas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pizzas",
                table: "Pizzas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pates",
                table: "Pates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pastas",
                table: "Pastas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "Pizzas",
                newName: "Pizza");

            migrationBuilder.RenameTable(
                name: "Pates",
                newName: "Pate");

            migrationBuilder.RenameTable(
                name: "Pastas",
                newName: "Pasta");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.RenameIndex(
                name: "IX_Pizzas_PateId",
                table: "Pizza",
                newName: "IX_Pizza_PateId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_PizzaId",
                table: "Ingredient",
                newName: "IX_Ingredient_PizzaId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_BurgerId",
                table: "Ingredient",
                newName: "IX_Ingredient_BurgerId");

            migrationBuilder.AddColumn<bool>(
                name: "EstVegetarien",
                table: "Ingredient",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pizza",
                table: "Pizza",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pate",
                table: "Pate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pasta",
                table: "Pasta",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Burger_BurgerId",
                table: "Ingredient",
                column: "BurgerId",
                principalTable: "Burger",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Pizza_PizzaId",
                table: "Ingredient",
                column: "PizzaId",
                principalTable: "Pizza",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pasta_Nourriture_Id",
                table: "Pasta",
                column: "Id",
                principalTable: "Nourriture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pizza_Nourriture_Id",
                table: "Pizza",
                column: "Id",
                principalTable: "Nourriture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pizza_Pate_PateId",
                table: "Pizza",
                column: "PateId",
                principalTable: "Pate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
