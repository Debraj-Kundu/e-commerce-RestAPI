using Inventory.API.Infrastructure;
using Inventory.API.Domain;
using MassTransit;
using MessageEvents;

namespace Inventory.API.Infrastructure
{
    public class InventoryRepository : IInventoryRepository
    {
        private List<ProductQuantityDomain> Inventory { get; set; }
        public InventoryRepository(InventoryData inventoryData) 
        {
            Inventory = inventoryData.Inventory;
        }

        public IEnumerable<ProductQuantityDomain> GetAll()
        {
            return Inventory;
        }
        public ProductQuantityDomain GetById(string id)
        {
            return Inventory.FirstOrDefault(u => u.Id.Equals(id));
        }

        public ProductQuantityDomain GetByProductId(string pId)
        {
            return Inventory.FirstOrDefault(u => u.ProductId.Equals(pId));
        }

        public bool CanPlaceOrder(string pId, int quantity)
        {
            var product = Inventory.FirstOrDefault(p => p.ProductId.Equals(pId));
            return product != null && product.Quantity >= quantity;
        }

        public void Add(ProductQuantityDomain item)
        {
            item.Id = Guid.NewGuid().ToString();
            Inventory.Add(item);
        }

        public async Task Update(string id, ProductQuantityDomain item)
        {
            int index = Inventory.FindIndex(x => x.ProductId.Equals(item.ProductId));
            Inventory[index].Quantity -= item.Quantity;
        }
        public async Task UpdateDetail(string id, ProductQuantityDomain item)
        {
            int index = Inventory.FindIndex(x => x.ProductId.Equals(item.ProductId));
            Inventory[index].Quantity = item.Quantity;
        }
        public void Delete(string id)
        {
            var itemToDelete = GetById(id);
            Inventory.Remove(itemToDelete);
        }

    }
}
