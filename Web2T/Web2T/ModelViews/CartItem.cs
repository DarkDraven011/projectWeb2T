using Web2T.Models;

namespace Web2T.ModelViews
{
    public class CartItem
    {
        public Product product { get; set; }
        public int amount { get; set; }
        public double discountPrice => (product.Price - ((product.Discount * 0.01) * product.Price)).Value;
        public double TotalMoney => amount * (product.Price - ((product.Discount * 0.01) * product.Price)).Value;
    }
}
