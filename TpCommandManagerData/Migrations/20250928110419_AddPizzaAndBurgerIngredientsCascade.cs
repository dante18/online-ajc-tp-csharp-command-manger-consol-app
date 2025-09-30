using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpCommandManagerData.Migrations
{
    /// <inheritdoc />
    public partial class AddPizzaAndBurgerIngredientsCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Burger_BurgerId",
                table: "Ingredients");

            migrationBuilder.AlterColumn<int>(
                name: "PizzaId",
                table: "Ingredients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Burger_BurgerId",
                table: "Ingredients",
                column: "BurgerId",
                principalTable: "Burger",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Burger_BurgerId",
                table: "Ingredients");

            migrationBuilder.AlterColumn<int>(
                name: "PizzaId",
                table: "Ingredients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Burger_BurgerId",
                table: "Ingredients",
                column: "BurgerId",
                principalTable: "Burger",
                principalColumn: "Id");
        }
    }
}
