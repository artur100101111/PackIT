using PackIt.Application.Orders.DTO;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Application.Orders.Queries
{
    public  class GetOrderTemplateByLocationQuery: IQuery<IEnumerable<OrderTemplateDto>>
    {
        public required string LocationCode { get; set; }
    }
}
