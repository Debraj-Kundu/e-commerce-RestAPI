
namespace MessageEvents
{
    public class CheckCartResponse
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool OrderPossible { get; set; }
    }
}
