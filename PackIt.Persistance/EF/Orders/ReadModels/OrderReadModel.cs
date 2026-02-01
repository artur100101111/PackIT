using PackIT.Domain.Orders.States;

namespace PackIt.Persistance.EF.Orders.ReadModels
{
    internal class OrderReadModel: OrderBaseReadModel
    {
        public LocationVoReadModel? DeliveryLocation { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public OrderStateEnum State { get; set; }

        public List<OrderStateChangedReadModel> StateChangesHistory { get; set; } = new();
    }
}
