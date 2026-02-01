namespace PackIT.Domain.Orders.States
{
    public class OrderStateService : IOrderStateService
    {
        private Dictionary<OrderStateEnum, OrderStateEnum[]> _states = new();

        public  OrderStateEnum InitialState => OrderStateEnum.New;

        /// <summary>
        /// 
        /// </summary>
        public OrderStateService()
        {
            _states.Add(OrderStateEnum.New, new[] { OrderStateEnum.New, OrderStateEnum.InPacking, OrderStateEnum.Canceled });//Order can be canceled at this stage
            _states.Add(OrderStateEnum.InPacking, new[] { OrderStateEnum.Packed, OrderStateEnum.Canceled });//Order can be canceled at this stage
            _states.Add(OrderStateEnum.Packed, new[] { OrderStateEnum.InDelivery, OrderStateEnum.Canceled });//Order can be canceled at this stage
            _states.Add(OrderStateEnum.InDelivery, new[] { OrderStateEnum.Delivered });
            _states.Add(OrderStateEnum.Delivered, Array.Empty<OrderStateEnum>());
        }


        public OrderStateEnum TryChangeState(Order order, OrderStateEnum newState)
        {
            if (_states[order.State].Contains(newState))
            {
               return newState;
            }
            throw new OrderStateIsNotAllowedException(order.Id.Value, order.State, newState);
        }
    }
}
