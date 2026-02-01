namespace PackIT.Domain.Orders.Primitives
{
    public class OrderItemPrimitive
    {
        public OrderItemPrimitive(long itemId, int quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }

        public long ItemId { get; }
        public int Quantity { get; }
    }
}