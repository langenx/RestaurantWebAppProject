using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantWebAppProject.Migrations
{
    /// <inheritdoc />
    public partial class adjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredients_Ingredients_ProductId",
                table: "ProductIngredients");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIngredients_IngredientId",
                table: "ProductIngredients",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredients_Ingredients_IngredientId",
                table: "ProductIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredients_Ingredients_IngredientId",
                table: "ProductIngredients");

            migrationBuilder.DropIndex(
                name: "IX_ProductIngredients_IngredientId",
                table: "ProductIngredients");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "Description",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredients_Ingredients_ProductId",
                table: "ProductIngredients",
                column: "ProductId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
