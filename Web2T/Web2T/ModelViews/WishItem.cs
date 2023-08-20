using Web2T.Models;

namespace Web2T.ModelViews
{
    public class WishItem
    {
        public Product product { get; set; }
        public double discountPrice => (product.Price - ((product.Discount * 0.01) * product.Price)).Value;
    }
}
