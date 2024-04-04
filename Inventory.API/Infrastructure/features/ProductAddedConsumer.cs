using Inventory.API.Domain;
using MassTransit;
using MessageEvents;

namespace Inventory.API.Infrastructure.features
{
    public class ProductAddedConsumer : IConsumer<ProductAddedEvent>
    {
        public IInventoryRepository InventoryRepository { get; set; }
        public ProductAddedConsumer(IInventoryRepository inventoryRepository)
        {

            InventoryRepository = inventoryRepository;

        }
        public Task Consume(ConsumeContext<ProductAddedEvent> context)
        {
            var productAddedEvent = context.Message;
            if (productAddedEvent != null)
            {
                InventoryRepository.Add(new ProductQuantityDomain
                {
                    ProductId = productAddedEvent.ProductId,
                    Quantity = productAddedEvent.Quantity,
                });
            }
            return Task.CompletedTask;
        }
    }
}
