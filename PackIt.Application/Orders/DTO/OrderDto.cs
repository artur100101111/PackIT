using PackIT.Domain.Orders.States;
using System.Text.Json.Serialization;

namespace PackIt.Application.Orders.DTO
{
    public class OrderDto : OrderBaseDTO
    {
        public LocationVoDTO DeliveryLocation { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public List<OrderStatedChangeDTO> StateChangesHistory { get; set; } = new();

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStateEnum State { get; set; }
    }
}
