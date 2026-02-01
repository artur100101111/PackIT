using PackIT.Domain.Orders.Exceptions;

namespace PackIT.Domain.Orders
{
    public record OrderId
    {
        public long Value { get; init; }

        public OrderId(long value)
        {
            if (value < 1)
            { 
                throw new OrderIdOutOfRangeException("Order Id value must be greater then 0.");
            }
            Value = value;
        }

        public static implicit operator OrderId(long id)
        {
            return new OrderId(id);
        }

        public static implicit operator long(OrderId id)
        {
            return id.Value;
        }
    }
}
