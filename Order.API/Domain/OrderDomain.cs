namespace Order.API.Domain
{
    public class OrderDomain
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalCost { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
