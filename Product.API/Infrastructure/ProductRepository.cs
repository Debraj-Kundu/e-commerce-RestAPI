using Product.API.Infrastructure;
using Product.API.Domain;
using MassTransit;
using MessageEvents;

namespace Product.API.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private List<ProductDomain> Products { get; set; }
        private readonly IPublishEndpoint _publishEndpoint;

        public ProductRepository(ProductsData productsData,IPublishEndpoint publishEndpoint) 
        {
            Products = productsData.Products;
            _publishEndpoint = publishEndpoint;
        }

        public IEnumerable<ProductDomain> GetAll()
        {
            return Products;
        }
        public ProductDomain GetById(string id)
        {
            return Products.FirstOrDefault(u => u.Id.Equals(id));
        }
        public async Task Add(ProductDomain product, int quantity)
        {
            product.Id = Guid.NewGuid().ToString();
            Products.Add(product);
            await _publishEndpoint.Publish(new ProductAddedEvent
            {
                ProductId = product.Id,
                Quantity = quantity,
            });
        }

        public async void Update(string id, ProductDomain product, int quantity)
        {
            int index = Products.FindIndex(u => u.Id.Equals(id));
            Products[index].Name = product.Name != null ? product.Name : Products[index].Name;
            Products[index].Size = product.Size != null ? product.Size : Products[index].Size;
            Products[index].Design = product.Design != null ? product.Design : Products[index].Design;
            Products[index].Price = product.Price != 0 ? product.Price : Products[index].Price;
            await _publishEndpoint.Publish(new ProductUpdatedEvent
            {
                ProductId = id,
                Quantity = quantity,
            });
        }

        public async void Delete(string id)
        {
            var productToDelete = GetById(id);
            await _publishEndpoint.Publish(new ProductDeletedEvent
            {
                ProductId = id
            });
            Products.Remove(productToDelete);
        }

    }
}
