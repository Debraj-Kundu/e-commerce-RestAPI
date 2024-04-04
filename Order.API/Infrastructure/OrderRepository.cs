using Order.API.Infrastructure;
using Order.API.Domain;
using MassTransit;
using MessageEvents;
using Order.API.Application.Models;
using System.Diagnostics;

namespace Order.API.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        private List<OrderDomain> Orders { get; set; }
        private readonly IRequestClient<CheckInventoryRequest> _client;
        private readonly IRequestClient<CheckCartRequest> _cartClient;


        public OrderRepository(OrderData orderData, IRequestClient<CheckInventoryRequest> client, IRequestClient<CheckCartRequest> cartClient)
        {
            Orders = orderData.Orders;
            _client = client;
            _cartClient = cartClient;
        }

        public IEnumerable<OrderDomain> GetAll()
        {
            return Orders;
        }
        public OrderDomain GetById(string id)
        {
            return Orders.FirstOrDefault(u => u.Id.Equals(id));
        }

        public IEnumerable<OrderDomain> GetByUserId(string uId)
        {
            return Orders.Where(u => u.UserId.Equals(uId)).ToList();
        }

        private async Task<CheckCartResponse> GetCartDetails(string userId)
        {
            var cartReq = new CheckCartRequest
            {
                UserId = userId,
            };
            var cart = await _cartClient.GetResponse<CheckCartResponse>(cartReq);
            return cart.Message;
        }

        public async Task<OrderResponse> Add(string userId)
        {
            var cart = GetCartDetails(userId).Result;
            if (!cart.OrderPossible) return new OrderResponse
            {
                Status = false,
                Message = "Nothing in cart to order."
            };

            var requestedOrder = new OrderDomain
            {
                Id = userId,
                UserId = cart.UserId,
                ProductId = cart.ProductId,
                Quantity = cart.Quantity,
                TotalCost = cart.TotalCost,
                CreatedAt = DateTime.UtcNow,
            };

            var req = new CheckInventoryRequest
            {
                OrderId = Guid.NewGuid().ToString(),
                UserId = requestedOrder.UserId,
                ProductId = requestedOrder.ProductId,
                Quantity = requestedOrder.Quantity,
            };

            var res = await _client.GetResponse<CheckInventoryResponse>(req);

            if (res.Message.IsValid)
                PlaceOrder(requestedOrder);
            if (res.Message.IsValid)
                return new OrderResponse
                {
                    Status = true,
                    Message = "Order placed successfully"
                };
            return new OrderResponse 
            { 
                Status = false, 
                Message = "Item is either not in stock or unavailable. Order was not placed." 
            };
        }

        public void PlaceOrder(OrderDomain? item)
        {
            if (item == null) return;
            Orders.Add(item);
        }

        public void Update(string id, OrderDomain item)
        {
            int index = Orders.FindIndex(x => x.Id.Equals(id));
            Orders[index].UserId = item.UserId;
        }

        public void Delete(string id)
        {
            var itemToDelete = GetById(id);
            Orders.Remove(itemToDelete);
        }


    }
}
