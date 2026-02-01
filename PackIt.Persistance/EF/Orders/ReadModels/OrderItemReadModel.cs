

namespace PackIt.Persistance.EF.Orders.ReadModels
{
    public class OrderItemReadModel
    {
        public int Quantity { get; set; }
        public ItemVOReadModel ItemVO { get; set; }
    }
}