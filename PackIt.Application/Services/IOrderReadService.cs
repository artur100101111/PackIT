using PackIT.Domain.Orders;
using PackIT.Domain.Orders.States;

namespace PackIt.Application.Services
{
    public interface IOrderReadService
    {
        /// <summary>
        /// Returns true when Order with the same initial data exists already.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="reqDelLocationCode"></param>
        /// <param name="RequestedDeliveryTime"></param>
        /// <param name="orderItems"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ExistsByLocationStatusItemsAsync
            ( OrderStateEnum state, string reqDelLocationCode, DateTime RequestedDeliveryTime, List<OrderItem> orderItems, CancellationToken cancellationToken);
    }
}
