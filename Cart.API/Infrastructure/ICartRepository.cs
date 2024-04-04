using Cart.API.Domain;

namespace Cart.API.Infrastructure
{
    public interface ICartRepository
    {
        IEnumerable<CartDomain> GetAll();
        CartDomain GetById(string id);
        Task<IEnumerable<CartDomain>> GetByUserId(string uId);
        Task Add(CartDomain order);
        Task<IEnumerable<CartDomain>> GetAndDeleteCart(string uId);
        void Update(string id, CartDomain order);
        void Delete(string id);
    }
}
