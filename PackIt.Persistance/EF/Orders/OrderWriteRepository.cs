using Microsoft.EntityFrameworkCore;
using PackIt.Application.Orders;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.Shared;
using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Orders;

namespace PackIt.Persistance.EF.Orders
{
    internal class OrderWriteRepository : WriteRepository<OrderBase, OrderId>, IOrderRepository
    {
        public OrderWriteRepository(WriteDbContext writeDbContext) 
            : base(writeDbContext)
        {

        }


        public async Task<bool> CheckIfExistsAsync<T>(ISpecyfication<T> existanceSpecyfication, CancellationToken cancellationToken = default) where T : OrderBase
        {
            IQueryable<T> query = _writeDbContext.Set<T>();
            query = query.Where(existanceSpecyfication.Criteria);
            return await query.AnyAsync(cancellationToken);
        }

        public async Task<T?> GetOrderBySpecyfictionAsync<T>(ISpecyfication<T> criteria, CancellationToken cancellationToken = default) where T : OrderBase
        { 
            IQueryable<T> query = _writeDbContext.Set<T>();

            query = query.Where(criteria.Criteria);

            foreach (var include in criteria.Includes)
                query = query.Include(include);

            var result = await query.FirstOrDefaultAsync(cancellationToken);
            return result;
        }

        public async Task<IEnumerable<T>> GetOrdersBySpecyfictionAsync<T>(ISpecyfication<T> criteria, CancellationToken cancellationToken = default) where T : OrderBase
        {
            IQueryable<T> query = _writeDbContext.Set<T>();

            query = query.Where(criteria.Criteria);

            foreach (var include in criteria.Includes)
                query = query.Include(include);

            var result = await query.ToListAsync(cancellationToken);
            return result;
        }
    }
}
