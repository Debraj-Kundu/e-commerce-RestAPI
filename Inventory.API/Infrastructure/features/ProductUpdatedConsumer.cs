using Inventory.API.Domain;
using MassTransit;
using MessageEvents;

namespace Inventory.API.Infrastructure.features
{
    public class ProductUpdatedConsumer : IConsumer<ProductUpdatedEvent>
    {
        public IInventoryRepository InventoryRepository { get; set; }
        public ProductUpdatedConsumer(IInventoryRepository inventoryRepository)
        {

            InventoryRepository = inventoryRepository;

        }
        public async Task Consume(ConsumeContext<ProductUpdatedEvent> context)
        {
            var productUpdatedEvent = context.Message;
            if (productUpdatedEvent != null)
            {
                await InventoryRepository.UpdateDetail(productUpdatedEvent.ProductId, new ProductQuantityDomain
                {
                    ProductId = productUpdatedEvent.ProductId,
                    Quantity = productUpdatedEvent.Quantity,
                });
            }
        }
    }
}
