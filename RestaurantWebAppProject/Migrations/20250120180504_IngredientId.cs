using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantWebAppProject.Migrations
{
    /// <inheritdoc />
    public partial class IngredientId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IngredientID",
                table: "Ingredients",
                newName: "IngredientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "Ingredients",
                newName: "IngredientID");
        }
    }
}
