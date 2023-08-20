using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web2T.Extension;
using Web2T.Models;
using Web2T.ModelViews;

namespace Web2T.Controllers
{
    public class WishListController : Controller
    {
        private readonly DbMarketsContext _context;
        public INotyfService _notyfService { get; }
        public WishListController(DbMarketsContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        public List<WishItem> List
        {
            get
            {
                var wl = HttpContext.Session.Get<List<WishItem>>("List");
                if (wl == default(List<WishItem>))
                {
                    wl = new List<WishItem>();
                }
                return wl;
            }
        }

        [HttpPost]
        [Route("api/wishlist/add")]
        public IActionResult AddToWishList(int productID)
        {
            List<WishItem> wishlist = List;

            try
            {
                //Them san pham vao gio hang
                WishItem item = wishlist.SingleOrDefault(p => p.product.ProductId == productID);
                if (item != null) // da co => cap nhat so luong
                {
                    //luu lai session
                    HttpContext.Session.Set<List<WishItem>>("List", wishlist);
                }
                else
                {
                    Product hh = _context.Products.SingleOrDefault(p => p.ProductId == productID);
                    item = new WishItem
                    {
                        product = hh
                    };
                    wishlist.Add(item);//Them vao gio
                }

                //Luu lai Session
                HttpContext.Session.Set<List<WishItem>>("List", wishlist);
                _notyfService.Success("Thêm sản phẩm vào Wish List thành công");
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [Route("api/wishlist/update")]
        public IActionResult UpdateWishList(int productID)
        {
            //Lay gio hang ra de xu ly
            var wishlist = HttpContext.Session.Get<List<WishItem>>("List");
            try
            {
                if (wishlist != null)
                {
                    WishItem item = wishlist.SingleOrDefault(p => p.product.ProductId == productID);
                    //Luu lai session
                    HttpContext.Session.Set<List<WishItem>>("List", wishlist);
                }
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [Route("api/wishlist/remove")]
        public ActionResult Remove(int productID)
        {

            try
            {
                List<WishItem> wishList = List;
                WishItem item = wishList.SingleOrDefault(p => p.product.ProductId == productID);
                if (item != null)
                {
                    wishList.Remove(item);
                }
                //luu lai session
                HttpContext.Session.Set<List<WishItem>>("List", wishList);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        [Route("wishlist.html", Name = "Wish List")]
        public IActionResult Index()
        {
            var collection = List;
            return View(collection);
        }
    }
}
