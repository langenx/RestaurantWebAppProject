using Microsoft.AspNetCore.Mvc;
using RestaurantWebAppProject.Data;
using RestaurantWebAppProject.Models;

namespace RestaurantWebAppProject.Controllers
{
    public class IngredientController : Controller
    {
        private Repository<Ingredient> ingredients;

        public IngredientController(ApplicationDbContext context)
        {
            ingredients = new Repository<Ingredient>(context);
        }
        public async Task<IActionResult> Index()
        {
            return View(await ingredients.GetAllAsync());
        }
        public async Task<IActionResult> Details(int id) //asynchroniczna metoda która pobiera informacje o Ingredient na podstawie id, zwraca widok Details, wykorzystuje mechanizm eager loading (includes) do pobrania danych powiązanych 
        {
            return View(await ingredients.GetByIdAsync(id, new QueryOptions<Ingredient>() { Includes = "ProductIngredients.Product" }));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //security token
        public async Task<IActionResult> Create([Bind("IngredientId, Name")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                await ingredients.AddAsync(ingredient);
                return RedirectToAction("Index");
            }
            return View(ingredient);
        }
    }
}
