using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Orders;
using System.Linq.Expressions;

namespace PackIt.Application.Orders.Commands.Specyfications
{
    internal class CheckIfOrderTemplateExistsByLocationAndName : ISpecyfication<OrderTemplate>
    {
        public string OrderName { get; }
        public string RequestedLocationCode { get; }

        public Expression<Func<OrderTemplate, bool>> Criteria => o => RequestedLocationCode == RequestedLocationCode && o.Name == OrderName;

        public List<Expression<Func<OrderTemplate, object>>> Includes => null;

        public CheckIfOrderTemplateExistsByLocationAndName(string orderName, string reqLocationCode)
        {
            OrderName = orderName;
            RequestedLocationCode = reqLocationCode;
        }
    }
}
