using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Web2T.Models;

namespace Web2T.Controllers
{
    public class SearchController : Controller
    {
        private readonly DbMarketsContext _context;
        public INotyfService _notyfService { get; }
        public SearchController(DbMarketsContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        
        
        [HttpPost]
        public IActionResult FindProduct(string keyword)
        {
            List<Product> products = new List<Product>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("_ProductsPartialView", null);
            }
            products = _context.Products.AsNoTracking()
                                  .Include(a => a.Cat)
                                  .Where(x => x.ProductName.Contains(keyword))
                                  .OrderByDescending(x => x.ProductName)
                                  .Take(10)
                                  .ToList();
            if (products == null)
            {
                return PartialView("_ProductsPartialView", null);
            }
            else
            {
                return PartialView("_ProductsPartialView", products);
            }
        }

        [HttpPost]
        public IActionResult FilterProducts(decimal minPrice, decimal maxPrice)
        {
            // Perform the filtering of products based on the price range
            var filteredProducts = _context.Products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
            return PartialView("_ProductsPartialView", filteredProducts);
        }

        [HttpPost]
        public IActionResult GetCheckedProducts(int? Categoryid)
        {
            var products = _context.Products
                .AsNoTracking()
                .Where(p => p.CatId == Categoryid)
                .Include(p => p.Cat)
                .OrderByDescending(p => p.ProductId).ToList();
            return PartialView("_ProductsPartialView", products);
        }
    }
}
