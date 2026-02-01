namespace PackIt.Application.Orders.DTO
{
    public abstract class OrderBaseDTO
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public LocationVoDTO RequestedDeliveryLocation { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
