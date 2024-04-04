namespace Inventory.API.Domain
{
    public class ProductQuantityDomain
    {
        public string Id { get; set; }

        public string ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
