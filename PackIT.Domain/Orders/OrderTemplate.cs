using PackIT.Domain.Orders.ValueObjects;

namespace PackIT.Domain.Orders
{
    public class OrderTemplate: OrderBase
    {
        public string Name { get; private set; }

        protected OrderTemplate():base() { }
        internal OrderTemplate(OrderId id, DateTime date, LocationVO location, List<OrderItem> items,  string name) :base(id, date, location, items)
        {
            this.Name = name;        
        }

        public override string ToString()
        {
            return $"OrderId: {this.Id}, OrderDate: {this.CreationDate}, OrderLocato: {this.RequestedDeliveryLocation}, OrderName: { Name}";
        }


    }
}
