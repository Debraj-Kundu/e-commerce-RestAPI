using Product.API.Domain;

namespace Product.API.Infrastructure
{
    public interface IProductRepository
    {
        IEnumerable<ProductDomain> GetAll();
        ProductDomain GetById(string id);
        Task Add(ProductDomain user, int quantity);
        void Update(string id, ProductDomain user, int quantity);
        void Delete(string id);
    }
}
