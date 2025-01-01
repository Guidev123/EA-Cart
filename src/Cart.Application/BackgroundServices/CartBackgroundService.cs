using Cart.Application.Events;
using Cart.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedLib.MessageBus;

namespace Cart.Application.BackgroundServices
{
    public class CartBackgroundService(IMessageBus bus, IServiceProvider serviceProvider) : BackgroundService
    {
        private readonly IMessageBus _bus = bus;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        private void SetSubscribers() =>
            _bus.SubscribeAsync<OrderPlacedIntegrationEvent>("OrderPlacedIntegrationEvent", DeleteCart);

        private async Task DeleteCart(OrderPlacedIntegrationEvent integrationEvent)
        {
            using var scope = _serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<ICartRepository>();

            var cart = await repository.GetByCustomerIdAsync(Guid.Parse(integrationEvent.CustomerId));
            if (cart is not null) await repository.DeleteWhenOrderFinished(cart);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask;
        }
    }
}
