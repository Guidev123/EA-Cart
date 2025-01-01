namespace Cart.Application.Events
{
    public class OrderPlacedIntegrationEvent : IntegrationEvent
    {
        public OrderPlacedIntegrationEvent(string customerId)
        {
            AggregateId = Guid.Parse(customerId);
            CustomerId = customerId;
        }
        public string CustomerId { get; private set; }
    }
}
