namespace Northwind.CustomerManagement.Api.DataMaintenance
{
    /// <summary>
    /// Represents a customer related event in the system.
    /// </summary>
    public sealed class CustomerEvent
    {
        internal CustomerEvent(string eventType, Customer newState)
        {
            EventType = eventType;
            NewState = newState;
        }

        /// <summary>
        /// Unique id for this even instance.
        /// </summary>
        public Guid EventId { get; } = Guid.NewGuid();
        
        /// <summary>
        /// They type of event.
        /// </summary>
        public string EventType { get; }

        /// <summary>
        /// The state of the customer when this event was raised.
        /// </summary>
        public Customer NewState { get; }
    }
}
