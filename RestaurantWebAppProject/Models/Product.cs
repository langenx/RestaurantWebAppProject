using System.ComponentModel;

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
        public Category? Category { get; set; } //produkt nalezy do 0 lub wielu kategorii
        public ICollection<OrderItem>? OrderItems { get; set; } //produkt moze byc w wielu itemach zamowienia
        public ICollection<ProductIngredient>? ProductIngredients {  get; set; } //produkt moze miec wiele składników
    }
}