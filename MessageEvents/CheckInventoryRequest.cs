
namespace MessageEvents
{
    public class CheckInventoryRequest
    {
        public string OrderId { get; set; }

        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
    }
}
