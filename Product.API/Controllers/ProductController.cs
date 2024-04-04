using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.Infrastructure;
using Product.API.Domain;

namespace Product.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IProductRepository ProductRepository { get; set; }
        public ProductController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        [HttpGet("products")]
        public IEnumerable<ProductDomain> GetProducts()
        {
            return ProductRepository.GetAll();
        }

        [HttpGet("product/{id}")]
        public ProductDomain GetProduct(string id)
        {
            return ProductRepository.GetById(id);
        }

        [HttpPost("product")]
        [Authorize(Roles = "admin")]
        public void AddProduct([FromBody] ProductDto product)
        {
            var productDomain = new ProductDomain
            {
                Id = product.Id,
                Name = product.Name,
                Size = product.Size,
                Price = product.Price,
                Design = product.Design,
            };
            ProductRepository.Add(productDomain, product.Quantity);
        }

        [HttpPut("product/{id}")]
        [Authorize(Roles = "admin")]
        public void UpdateProduct(string id, [FromBody] ProductDto product)
        {
            var productDomain = new ProductDomain
            {
                Id = product.Id,
                Name = product.Name,
                Size = product.Size,
                Price = product.Price,
                Design = product.Design,
            };
            ProductRepository.Update(id, productDomain, product.Quantity);
        }

        [HttpDelete("product/{id}")]
        [Authorize(Roles ="admin")]
        public void DeleteProduct(string id)
        {
            ProductRepository.Delete(id);
        }
    }
}
