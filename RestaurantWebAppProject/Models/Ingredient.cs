using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RestaurantWebAppProject.Models
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }

        [ValidateNever]
        public ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}