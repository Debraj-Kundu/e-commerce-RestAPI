using Order.API.Application.Models;
using Order.API.Domain;

namespace Order.API.Infrastructure
{
    public interface IOrderRepository
    {
        IEnumerable<OrderDomain> GetAll();
        OrderDomain GetById(string id);
        IEnumerable<OrderDomain> GetByUserId(string uId);
        Task<OrderResponse> Add(string userId);
        void PlaceOrder(OrderDomain? order);
        void Update(string id, OrderDomain order);
        void Delete(string id);
    }
}
