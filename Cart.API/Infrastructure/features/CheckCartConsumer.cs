using MassTransit;
using MessageEvents;

namespace Cart.API.Infrastructure.features
{
    public class CheckCartConsumer : IConsumer<CheckCartRequest>
    {
        public ICartRepository CartRepository { get; private set; }
        public CheckCartConsumer(ICartRepository cartRepository)
        {
            CartRepository = cartRepository;
        }


        public async Task Consume(ConsumeContext<CheckCartRequest> context)
        {
            var checkCartEvent = context.Message;
            if (checkCartEvent != null)
            {
                var cartItem = CartRepository.GetAndDeleteCart(checkCartEvent.UserId).Result.FirstOrDefault();
                CheckCartResponse res = new CheckCartResponse();
                if (cartItem != null)
                {
                    res = new CheckCartResponse
                    {
                        UserId = cartItem.UserId,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        TotalCost = cartItem.TotalCost,
                        CreatedAt = cartItem.CreatedAt,
                        OrderPossible = true
                    };
                    await context.RespondAsync(res);
                    return;
                }
                res.OrderPossible = false;
                await context.RespondAsync(res);

            }
            Task.CompletedTask.Wait();
        }
    }
    
}
