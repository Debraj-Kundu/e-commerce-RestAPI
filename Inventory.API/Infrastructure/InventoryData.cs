using Inventory.API.Domain;

namespace Inventory.API.Infrastructure
{
    public class InventoryData
    {
        public List<ProductQuantityDomain> Inventory { get; set; }
        public InventoryData()
        {
            Inventory = new List<ProductQuantityDomain>()
            {
                new ProductQuantityDomain()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = "9a7178ad-cdbc-4beb-8ccb-bff2b2e79c9a",
                    Quantity = 100,
                },
                new ProductQuantityDomain()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = "0350580e-1c8a-40f5-89b5-fa9cdfbfa84e",
                    Quantity = 50,
                },
                new ProductQuantityDomain()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = "4ef23580-27cc-438c-81b8-c1a530f7ca00",
                    Quantity = 10,
                },
                new ProductQuantityDomain()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = "0f14e5e3-116f-4cc1-91c9-4572bdf4e2a7",
                    Quantity = 300,
                }
            };
        }
    }
}
