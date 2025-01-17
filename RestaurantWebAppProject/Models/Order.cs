namespace RestaurantWebAppProject.Models
{
    public class Order
    {
        public int OrderId { get; set; } //pm
        public DateTime OrderDate { get; set; }
        public string? UserId { get; set; } //every order has one user // fk
        public ApplicationUser User { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } //zamowienie posiada 1 lub wiecej itemów

    }
}
