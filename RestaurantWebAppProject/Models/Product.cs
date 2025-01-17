using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantWebAppProject.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }

        [NotMapped]
        public IFormFile? Imagefile { get; set; }
        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";

        [ValidateNever]
        public Category? Category { get; set; } //produkt nalezy do 0 lub wielu kategorii

        [ValidateNever]
        public ICollection<OrderItem>? OrderItems { get; set; } //produkt moze byc w wielu itemach zamowienia

        [ValidateNever]
        public ICollection<ProductIngredient>? ProductIngredients {  get; set; } //produkt moze miec wiele składników
    }
}