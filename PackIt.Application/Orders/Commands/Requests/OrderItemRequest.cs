namespace PackIt.Application.Orders.Commands.Requests
{
    public class OrderItemRequest
    {
        public OrderItemRequest(long itemId, int quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }

        public long ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
