namespace Product.API.Domain
{
    public class ProductDto
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Size { get; set; }

        public decimal Price { get; set; }

        public string? Design { get; set; }

        public int Quantity { get; set; }
    }
}
