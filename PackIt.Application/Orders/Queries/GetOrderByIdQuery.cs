using PackIt.Application.Orders.DTO;
using PackIt.Shared.Abstractions.Queries;
using PackIT.Domain.Orders;

namespace PackIt.Application.Orders.Queries
{
    public class GetOrderByIdQuery: IQuery<OrderDto>
    {
        public required  long OrderId { get; set; }
    }
}
