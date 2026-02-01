using PackIT.Domain.Common;
using PackIT.Domain.Orders.States;

namespace PackIT.Domain.Orders.ValueObjects
{
    /// <summary>
    /// for simplicity one OrderStateChanged instead of separate  Order Events like OrderPacked, OrderDelivered etc,
    /// </summary>
    public record OrderStateChangedEvent : IDomainEvent
    {
        private OrderStateChangedEvent()
        {
            
        }
        public OrderStateChangedEvent(OrderStateEnum previousState, OrderStateEnum currentState, DateTime eventTime)
        {
            PreviousState = previousState;
            CurrentState = currentState;
            EventTime = eventTime;
        }
        public OrderStateEnum PreviousState { get; private set; }
        public OrderStateEnum CurrentState { get; private set; }
        public DateTime EventTime { get; private set; }

        ///public string OperatorId { get; set; }
        ///public Location? DeliverLocation { get; set; }
    }
}
