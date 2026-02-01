using PackIt.Application.Orders.DTO;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Application.Orders.Queries
{
    public class GetOrdersByRequestedLocationQuery: IQuery<IEnumerable<OrderDto>>
    {
        public string LocationCode { get; set; }
    }
}
