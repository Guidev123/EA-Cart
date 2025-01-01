namespace Cart.Application.Events
{
    public abstract class IntegrationEvent 
    {
        protected IntegrationEvent()
        {
            MessageType = GetType().Name;
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }
    }
}
