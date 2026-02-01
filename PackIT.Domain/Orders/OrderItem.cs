using PackIT.Domain.Orders.ValueObjects;

namespace PackIT.Domain.Orders
{
    public record OrderItem
    {
        protected OrderItem()
        {
            
        }
        public OrderItem(ItemVO item, int quantity)
        {
            ItemVO = item;
            Quantity = quantity;
        }

        public ItemVO ItemVO { get; init; }

        public int Quantity { get; private set; }

        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }
        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }


}
