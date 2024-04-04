using MassTransit;
using MessageEvents;

namespace Inventory.API.Infrastructure.features
{
    public class CheckInventoryConsumer : IConsumer<CheckInventoryRequest>
    {
        public IInventoryRepository InventoryRepository { get; set; }
        public CheckInventoryConsumer(IInventoryRepository inventoryRepository)
        {

            InventoryRepository = inventoryRepository;

        }
        public async Task Consume(ConsumeContext<CheckInventoryRequest> context)
        {
            var checkInventoryEvent = context.Message;
            if (checkInventoryEvent != null)
            {
                var isValid = InventoryRepository.CanPlaceOrder(checkInventoryEvent.ProductId, checkInventoryEvent.Quantity);
                if (isValid)
                {
                    await InventoryRepository.Update(checkInventoryEvent.UserId, new Domain.ProductQuantityDomain
                    {
                        ProductId = checkInventoryEvent.ProductId,
                        Quantity = checkInventoryEvent.Quantity,
                    });

                }
                var res = new CheckInventoryResponse
                {
                    OrderId = checkInventoryEvent.OrderId,
                    IsValid = isValid
                };
                await context.RespondAsync(res);
            }
        }
    }
}
