using Order.API.Domain;

namespace Order.API.Infrastructure
{
    public class OrderData
    {
        public List<OrderDomain> Orders { get; set; }
        public OrderData()
        {
            Orders = new List<OrderDomain>();
        }
    }
}
