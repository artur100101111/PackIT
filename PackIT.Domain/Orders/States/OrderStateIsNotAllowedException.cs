using PackIT.Domain.Common;

namespace PackIT.Domain.Orders.States
{
    public class OrderStateIsNotAllowedException : PackItException
    {
        public  long _orderId { get; }
        public OrderStateEnum _currentState { get; }
        public OrderStateEnum _newState { get; }
        public OrderStateIsNotAllowedException(long orderId, OrderStateEnum currentState, OrderStateEnum newState)
            : base($"OrderId: {orderId} with Current State: {currentState} cannot change state to New State: {newState}")
        {
            _orderId = orderId;
            _currentState = currentState;
            _newState = newState;
        }
    }
}
