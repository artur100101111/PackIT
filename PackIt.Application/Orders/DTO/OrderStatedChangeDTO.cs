using PackIT.Domain.Orders.States;
using System.Text.Json.Serialization;

namespace PackIt.Application.Orders.DTO
{
    public class OrderStatedChangeDTO
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStateEnum PreviousState { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStateEnum CurrentState { get; set; }
        public DateTime EventTime { get; set; }
    }
}