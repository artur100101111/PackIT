using PackIt.Shared.Abstractions.Persistance;
using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Orders;

namespace PackIt.Application.Orders
{
    public interface IOrderRepository : IWriteRepository<OrderBase, OrderId>
    {
        Task<T?> GetOrderBySpecyfictionAsync<T>(ISpecyfication<T> filterSpecylfication, CancellationToken cancellationToken = default) where T : OrderBase;
        Task<IEnumerable<T>> GetOrdersBySpecyfictionAsync<T>(ISpecyfication<T> filterSpecylfication, CancellationToken cancellationToken = default) where T : OrderBase;
        Task<bool> CheckIfExistsAsync<T>(ISpecyfication<T> existanceSpecyfication, CancellationToken cancellationToken = default) where T : OrderBase;

    }
}
