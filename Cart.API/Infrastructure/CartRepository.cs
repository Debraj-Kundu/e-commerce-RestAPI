using Cart.API.Infrastructure;
using Cart.API.Domain;
using MassTransit;
using MessageEvents;

namespace Cart.API.Infrastructure
{
    public class CartRepository : ICartRepository
    {
        private List<CartDomain> Carts { get; set; }
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<GetProductDetailRequest> _client;


        public CartRepository(CartData orderData, IPublishEndpoint client, IRequestClient<GetProductDetailRequest> reqClient) 
        {
            Carts = orderData.Carts;
            _publishEndpoint = client;
            _client = reqClient;
        }

        public IEnumerable<CartDomain> GetAll()
        {
            return Carts;
        }
        public CartDomain GetById(string id)
        {
            return Carts.FirstOrDefault(u => u.Id.Equals(id));
        }

        public async Task<IEnumerable<CartDomain>> GetByUserId(string uId)
        {
            var res = Carts.Where(u => u.UserId.Equals(uId)).ToList();
            res = await GetProductDetail(res);
            return res;
        }

        public async Task<IEnumerable<CartDomain>> GetAndDeleteCart(string uId)
        {
            var res = Carts.Where(u => u.UserId.Equals(uId)).ToList();
            res = await GetProductDetail(res);
            Carts.RemoveAll(u => u.UserId.Equals(uId));
            return res;
        }

        private async Task<List<CartDomain>> GetProductDetail(List<CartDomain> res)
        {
            if (res.Count > 0)
            {
                var prodDetail = await _client.GetResponse<GetProductDetailResponse>(new GetProductDetailRequest { ProductId = res.First().ProductId });
                res.First().TotalCost = prodDetail.Message.Price * res.First().Quantity;
            }
            return res;
        }

        public async Task Add(CartDomain item)
        {
            item.CreatedAt = DateTime.Now;
            item.Id = Guid.NewGuid().ToString();
            Carts.Add(item);
            var req = new CheckCartResponse
            {
                UserId = item.UserId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                CreatedAt = item.CreatedAt,
            };
            await _publishEndpoint.Publish(req);
            
        }

        public void Update(string id, CartDomain item)
        {
            int index = Carts.FindIndex(x => x.Id.Equals(id));
            Carts[index].ProductId = item.ProductId;
            Carts[index].Quantity = item.Quantity;
            item.CreatedAt = DateTime.Now;
        }

        public void Delete(string id)
        {
            var itemToDelete = GetById(id);
            Carts.Remove(itemToDelete);
        }

        
    }
}
