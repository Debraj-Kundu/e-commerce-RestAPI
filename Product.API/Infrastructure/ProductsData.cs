using Product.API.Domain;

namespace Product.API.Infrastructure
{
    public class ProductsData
    {
        public List<ProductDomain> Products { get; set; }
        public ProductsData() 
        {
            Products = new List<ProductDomain>()
            {
                new ProductDomain()
                {
                    Id = "9a7178ad-cdbc-4beb-8ccb-bff2b2e79c9a",
                    Name = "Painting",
                    Size = "Xl",
                    Price = 1000,
                    Design = "Floral"
                },
                new ProductDomain()
                {
                    Id = "0350580e-1c8a-40f5-89b5-fa9cdfbfa84e",
                    Name = "Guitar",
                    Size = "s",
                    Price = 2000,
                    Design = "Funky"
                },
                new ProductDomain()
                {
                    Id = "4ef23580-27cc-438c-81b8-c1a530f7ca00",
                    Name = "Vase",
                    Size = "l",
                    Price = 1200,
                    Design = "Elegant"
                },
                new ProductDomain()
                {
                    Id = "0f14e5e3-116f-4cc1-91c9-4572bdf4e2a7",
                    Name = "Silk Fabric",
                    Size = "Xxl",
                    Price = 1800,
                    Design = "Smooth"
                }
            };
        }
    }
}
