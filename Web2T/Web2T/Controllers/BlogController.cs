using Microsoft.AspNetCore.Mvc;

namespace Web2T.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
