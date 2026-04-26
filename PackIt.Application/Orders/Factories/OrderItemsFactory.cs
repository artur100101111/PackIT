using PackIt.Application.Items;
using PackIt.Application.Items.Commands.Specyfications;
using PackIt.Application.Items.Exceptions;
using PackIt.Application.Orders.Commands.CreateOrder;
using PackIT.Domain.Orders;
using PackIT.Domain.Orders.Primitives;

namespace PackIt.Application.Orders.Factories
{
    internal class OrderItemsFactory : IOrderItemsFactory
    {
        private IItemRepository _itemRepository;

        public OrderItemsFactory(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<List<OrderItem>> CreateOrderItemsAsync(List<OrderItemPrimitive> orderItemPrimitives, CancellationToken cancellationToken = default)
        {
            var orderItems = new List<OrderItem>();

            var itemIds = orderItemPrimitives.Select(i => i.ItemId).ToList();

            var items = await _itemRepository.GetItemsBySpecyfictionAsync(new GetItemsByIDListSpecyfication(itemIds));

            var except = itemIds.Except(items.Select(i => i.Id.Value).ToList());
            
            if (except.Count() > 0)
            {
               string noItems = string.Join(", ", except);
                throw new ItemNotFoundException($"Item with Id: {noItems} was not found.");
            }

            foreach (var orderItemPrimitive in orderItemPrimitives)
            {
                var item = items.First(i => i.Id == orderItemPrimitive.ItemId);

                orderItems.Add(new OrderItem(item.ToValueObject(), orderItemPrimitive.Quantity));
            }
            return orderItems;
        }

    }
}
