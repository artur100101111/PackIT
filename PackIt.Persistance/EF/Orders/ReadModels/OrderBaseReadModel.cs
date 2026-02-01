namespace PackIt.Persistance.EF.Orders.ReadModels
{
    internal abstract class OrderBaseReadModel
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public List<OrderItemReadModel> OrderItems { get; set; }
        public LocationVoReadModel RequestedDeliveryLocation { get; set; }

    }
}