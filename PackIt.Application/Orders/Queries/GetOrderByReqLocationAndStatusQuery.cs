using PackIt.Application.Orders.DTO;
using PackIt.Shared.Abstractions.Queries;
using PackIT.Domain.Orders.States;

namespace PackIt.Application.Orders.Queries
{
    public class GetOrdersByRequestedLocationAndStatusQuery: IQuery<IEnumerable<OrderDto>>
    {
        public required string LocationCode { get; set; }
        public required OrderStateEnum OrderState{ get; set; }
    }
}
