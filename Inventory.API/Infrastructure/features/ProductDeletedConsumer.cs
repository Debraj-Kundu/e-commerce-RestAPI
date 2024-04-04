using Inventory.API.Domain;
using MassTransit;
using MessageEvents;

namespace Inventory.API.Infrastructure.features
{
    public class ProductDeletedConsumer : IConsumer<ProductDeletedEvent>
    {
        public IInventoryRepository InventoryRepository { get; set; }
        public ProductDeletedConsumer(IInventoryRepository inventoryRepository)
        {

            InventoryRepository = inventoryRepository;

        }
        public async Task Consume(ConsumeContext<ProductDeletedEvent> context)
        {
            var productUpdatedEvent = context.Message;
            if (productUpdatedEvent != null)
            {
                InventoryRepository.Delete(productUpdatedEvent.ProductId);
            }
        }
    }
}
