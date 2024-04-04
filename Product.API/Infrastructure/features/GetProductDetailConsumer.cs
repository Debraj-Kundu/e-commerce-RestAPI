using MassTransit;
using MessageEvents;

namespace Product.API.Infrastructure.features
{
    public class GetProductDetailConsumer : IConsumer<GetProductDetailRequest>
    {
        public IProductRepository ProductRepository { get; private set; }

        public GetProductDetailConsumer(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }


        public async Task Consume(ConsumeContext<GetProductDetailRequest> context)
        {
            var getProductDetailMessage = context.Message;
            if(getProductDetailMessage != null) 
            {
                var product = ProductRepository.GetById(getProductDetailMessage.ProductId); 
                if(product != null)
                {
                    var res = new GetProductDetailResponse
                    {
                        Price = product.Price
                    };
                    await context.RespondAsync(res);
                }
            }
        }
    }
}
