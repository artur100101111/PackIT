using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Orders;
using System.Linq.Expressions;

namespace PackIt.Application.Orders.Commands.Specyfications
{
    internal class GetOrderTemplateByIdSpecyfication : ISpecyfication<OrderTemplate>
    {
        public OrderId Id { get; set; }
        public Expression<Func<OrderTemplate, bool>> Criteria => o => o.Id == Id;
        public List<Expression<Func<OrderTemplate, object>>> Includes => new();
        public Expression<Func<OrderTemplate, object>>? Selector => null;

        public GetOrderTemplateByIdSpecyfication(OrderId orderId)
        {
            Id = orderId;
        }
    }
}
