using Microsoft.AspNetCore.Identity;

namespace RestaurantWebAppProject.Models
{
    public class ApplicationUser : IdentityUser //rozszerzona klasa użytkownika oparta na Identity, służy do zarządzania autoryzacją i uwierzytelnianiem użytknowników
    {
        public ICollection<Order> Orders { get; set; } //relacja jeden do wielu pomiędzy użytknownikiem a zamówieniami (jeden użytknownik ma 0 lub więcej zamówień)

        // Zawiera podstawowe właściwości takie jak: Username, Email, PasswordHash, PhoneNumber, Id
    }
}
