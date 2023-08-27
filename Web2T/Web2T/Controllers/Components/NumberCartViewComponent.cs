using Microsoft.AspNetCore.Mvc;
using Web2T.ModelViews;
using Web2T.Extension;

namespace Web2T.Controllers.Components
{
    public class NumberCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            return View(cart);
        }
    }
}
