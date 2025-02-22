﻿namespace RestaurantWebAppProject.Models
{
    public class OrderViewModel //view model
    {
        public decimal TotalAmount {  get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        public IEnumerable<Product> Products { get; set; }  
    }
}
