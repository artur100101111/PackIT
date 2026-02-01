using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Orders;
using System.Linq.Expressions;

namespace PackIt.Application.Orders.Commands.Specyfications
{
    internal class GetOrderByIdSpecyfication : ISpecyfication<Order>
    {
        public OrderId Id { get; set; }
        public Expression<Func<Order, bool>> Criteria => o => o.Id == Id;
        public List<Expression<Func<Order, object>>> Includes =>  new();
        public Expression<Func<Order, object>>? Selector => null;

        public GetOrderByIdSpecyfication(OrderId orderId)
        {
            Id = orderId;
        }
    }
}
