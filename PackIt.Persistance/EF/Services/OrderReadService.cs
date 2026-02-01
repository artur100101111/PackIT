using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PackIt.Application.Services;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.Orders.ReadModels;
using PackIt.Persistance.EF.Shared;
using PackIT.Domain.Orders;
using PackIT.Domain.Orders.States;
using System.Data;

namespace PackIt.Persistance.EF.Services
{
    internal class OrderReadService : IOrderReadService
    {
        private ReadDbContext _readDbContext;
        private DbSet<OrderReadModel> _orders;


        public OrderReadService(ReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
            _orders = readDbContext.Orders;
        }
        /// <summary>
        /// Checking if Order with the same State, set of Items, CreationDate, RequestedLocation exists .
        /// </summary>
        /// <param name="State"></param>
        /// <param name="ReqDelLocationCode"></param>
        /// <param name="RequestedDeliveryTime"></param>
        /// <param name="OrderItems"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> ExistsByLocationStatusItemsAsync(OrderStateEnum State, string ReqDelLocationCode, DateTime RequestedDeliveryTime,  List<OrderItem> OrderItems, CancellationToken cancellationToken)
        {
            var oItemsSqlTable = new DataTable();
            oItemsSqlTable.Columns.Add("Code", typeof(string));
            oItemsSqlTable.Columns.Add("Quantity", typeof(int));

            foreach (var item in OrderItems)
            {
                oItemsSqlTable.Rows.Add(item.ItemVO.Code, item.Quantity);  
            }

            var spItemKeys = new SqlParameter("@OrderItemKeys", oItemsSqlTable)
            {
                TypeName = "[packing].[OrderItemKeys]",
                SqlDbType = SqlDbType.Structured
            };
            var spState = new SqlParameter("@OrderStatus", State.ToString());
            var spLocationCode= new SqlParameter("@RequestedLocationCode", ReqDelLocationCode);
            var spRequestedDeliveryTime = new SqlParameter("@RequestedDeliveryTime", RequestedDeliveryTime);

            var orderExists =  await _readDbContext.Set<BoolResult>().FromSqlRaw(@"EXEC [packing].[GetOrdersByStatusAndItems] 
                    @OrderStatus, 
                    @RequestedLocationCode, 
                    @RequestedDeliveryTime,
                    @OrderItemKeys",
            spState, spLocationCode, spRequestedDeliveryTime, spItemKeys).ToListAsync();


            return orderExists.Single().Result;
        }
    }
}
