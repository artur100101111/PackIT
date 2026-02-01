using PackIT.Domain.Orders.States;

namespace PackIt.Persistance.EF.Orders.ReadModels
{
    public class OrderStateChangedReadModel
    {
        public OrderStateEnum PreviousState { get; set; }
        public OrderStateEnum CurrentState { get; set; }
        public DateTime EventTime { get; set; }
    }
}