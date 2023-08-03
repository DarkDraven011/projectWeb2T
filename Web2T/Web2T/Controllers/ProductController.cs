using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PagedList.Core;
using Web2T.Models;

namespace Web2T.Controllers
{
    public class ProductController : Controller
    {
        private readonly DbMarketsContext _context;
        public ProductController(DbMarketsContext context)
        {
            _context = context;
        }
        [Route("shop.html", Name = "ShopProduct")]
        public IActionResult Index(int? page, string keyword="")
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 10;

                List<Product> products = new List<Product>();

                if(!string.IsNullOrEmpty(keyword))
                {
                    products = _context.Products
                                    .Where(x=>x.ProductName.Contains(keyword) && x.Active==true)
                                    .AsNoTracking()
                                    .OrderByDescending(x => x.DateCreated)
                                    .ToList();
                }
                else
                {
                    products = _context.Products
                                        .Where(x=>x.Active==true)
                                        .AsNoTracking()
                                        .OrderByDescending(x => x.DateCreated)
                                        .ToList();
                }


                PagedList<Product> models = new PagedList<Product>(products.AsQueryable(), pageNumber, pageSize);

                ViewBag.CurrentPage = pageNumber;
                ViewBag.CurrentKeyword = keyword;
                return View(models);
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
                return RedirectToAction("Index", "Home");
            }

        }
        [Route("danhmuc/{Alias}", Name = "ListProduct")]
        public IActionResult List(string Alias, int page = 1)
        {
            try
            {
                var pageSize = 10;
                var danhmuc = _context.Categories.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);
                var lsProducts = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CatId == danhmuc.CatId)
                    .OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(lsProducts, page, pageSize);

                ViewBag.CurrentPage = page;
                ViewBag.CurrentCat = danhmuc;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [Route("/{Alias}-{id}.html", Name = "ProductDetails")]
        public IActionResult Details(int id)
        {
            try
            {
                var product = _context.Products.Include(x => x.Cat).FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }

                var lsProduct = _context.Products.AsNoTracking()
                    .Where(x => x.CatId == product.CatId && x.ProductId != id && x.Active == true)
                    .OrderByDescending(x => x.DateCreated)
                    .Take(4)
                    .ToList();

                ViewBag.SanPham = lsProduct;

                decimal discountPrice = 0;

                if(product.Discount!=null && product.Discount>0)
                {
                    var giamgia =(double)product.Discount * 0.01;
                    var sotiengiam = product.Price * giamgia;
                    discountPrice = (decimal)(product.Price - sotiengiam);
                }
                ViewBag.discountPrice = discountPrice;

                return View(product);
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
               
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
