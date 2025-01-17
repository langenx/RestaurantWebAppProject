namespace RestaurantWebAppProject.Models
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }
        public ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}