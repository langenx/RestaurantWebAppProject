using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantWebAppProject.Data;
using RestaurantWebAppProject.Models;
using System.Net.Http.Headers;

namespace RestaurantWebAppProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Repository<Product> _products;
        private Repository<Order> _orders;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _products = new Repository<Product>(context);
            _orders = new Repository<Order>(context);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products = await _products.GetAllAsync()
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddItem(int prodId, int prodQty)
        {
            var product = await _context.Products.FindAsync(prodId);
            if (product == null)
            {
                return NotFound();
            }


            //wycgiagamy dane z widoku sesji jesli jest null tworzymy nowy orderviewmodel
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products = await _products.GetAllAsync()
            };

            //sprawdzamy czy produkt jest juz w orderitems
            var existingItem = model.OrderItems.FirstOrDefault(oi => oi.ProductId == prodId);

            //jesli produkt jest juz w zamowieniu, uaktualniamy quantity
            if (existingItem != null)
            {
                existingItem.Quantity += prodQty;
            }
            else
            {
                model.OrderItems.Add(new OrderItemViewModel
                {
                    ProductId = product.ProductId,
                    Price = product.Price,
                    Quantity = prodQty,
                    ProductName = product.Name
                });
            }

            // update calej kwoty zamowienia
            model.TotalAmount = model.OrderItems.Sum(oi => oi.Price * oi.Quantity);

            //zapisujemy updated orderviewmodel dla sesji
            HttpContext.Session.Set("OrderViewModel", model);

            return RedirectToAction("Create", model);

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Cart()
        {
            //pobieranie orderviewmodel z sesji
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");

            if (model == null || model.OrderItems.Count == 0)
            {
                return RedirectToAction("Create");
            }
            
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PlaceOrder()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");
            if (model == null || model.OrderItems.Count == 0)
            {
                return RedirectToAction("Create");
            }

            //tworzenie nowego order
            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                TotalAmount = model.TotalAmount,
                UserId = _userManager.GetUserId(User)
            };

            //dodawanie orderitems do order
            foreach (var item in model.OrderItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId= item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                });
            }

            //zapisywanie order do bazy danych
            await _orders.AddAsync(order);


            //clearowanie sesji
            HttpContext.Session.Remove("OrderViewModel");

            return RedirectToAction("ViewOrders")
;        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ViewOrders()
        {
            var userId = _userManager.GetUserId(User);
            
            var userOrders = await _orders.GetAllByIdAsync(userId, "UserId", new QueryOptions<Order>
            {
                Includes = "OrderItems.Product"
            });
            return View(userOrders);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
