namespace PackIT.Domain.Orders.States
{
    public interface IOrderStateService
    {
        OrderStateEnum InitialState { get; }
        OrderStateEnum TryChangeState(Order order, OrderStateEnum newState);
    }
}
