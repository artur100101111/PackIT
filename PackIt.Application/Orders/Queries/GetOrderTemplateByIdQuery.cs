using PackIt.Application.Orders.DTO;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Application.Orders.Queries
{
    public class GetOrderTemplateByIdQuery: IQuery<OrderTemplateDto>
    {
        public required long OrderId { get; set; }
    }

    //abstract więc query przekazane do infrastruct
}
