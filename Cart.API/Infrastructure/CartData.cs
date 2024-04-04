using Cart.API.Domain;

namespace Cart.API.Infrastructure
{
    public class CartData
    {
        public List<CartDomain> Carts { get; set; }
        public CartData()
        {
            Carts = new List<CartDomain>();
        }
    }
}
