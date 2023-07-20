using Web2T.Models;

namespace Web2T.ModelViews
{
    public class HomeViewVM
    {
        public List<Post> Posts { get; set; }
        public List<ProductHomeVM> Products { get; set; }
    }
}
