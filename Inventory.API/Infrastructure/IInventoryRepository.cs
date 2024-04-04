using Inventory.API.Domain;

namespace Inventory.API.Infrastructure
{
    public interface IInventoryRepository
    {
        IEnumerable<ProductQuantityDomain> GetAll();
        ProductQuantityDomain GetById(string id);
        ProductQuantityDomain GetByProductId(string pId);
        bool CanPlaceOrder(string pId, int quantity);
        void Add(ProductQuantityDomain user);
        Task Update(string id, ProductQuantityDomain user);
        Task UpdateDetail(string id, ProductQuantityDomain item);
        void Delete(string id);
    }
}
