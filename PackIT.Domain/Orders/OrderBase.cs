using PackIT.Domain.Common;
using PackIT.Domain.Orders.Events;
using PackIT.Domain.Orders.Exceptions;
using PackIT.Domain.Orders.ValueObjects;
using System.Data.Common;

namespace PackIT.Domain.Orders
{
    public abstract class OrderBase : AggregateRoot<OrderId>, IEntity<OrderId>
    {
        protected OrderBase():base() { }
        protected OrderBase(OrderId Id, DateTime date, LocationVO requestedDeliveryLocation, List<OrderItem> items)
        {
            this.Id = Id;
            this.CreationDate = date;
            this.RequestedDeliveryLocation = requestedDeliveryLocation;


            if (items == null || items.Count == 0)
                throw new OrderMustHaveAtLeastOneItemException(items is null ? "Items list is null" : "Items list is empty");

            foreach (var item in items)
            {
                this.AddItem(item);
            }

        }

        public LocationVO RequestedDeliveryLocation { get; private set; }
        public DateTime CreationDate { get; private set; }

        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems;

        public virtual void AddItem(OrderItem orderItem)
        {
            var existingItemInOrder = GetItem(orderItem.ItemVO.Code);

            if (existingItemInOrder != null)
            {
                existingItemInOrder.IncreaseQuantity(orderItem.Quantity);
                AddEvent(new OrderItemUpdated(this, orderItem));
            }
            else
            {
                _orderItems.Add(orderItem);
                AddEvent(new OrderItemAdded(this, orderItem));
            }


        }

        private OrderItem? GetItem(string itemCode)
        {
            var orderItem = _orderItems.FirstOrDefault(i => i.ItemVO.Code == itemCode);
            return orderItem;
        }

        public virtual void RemoveItem(string itemCode)
        {
            var orderItemToRemove = this.GetItem(itemCode);
            if (orderItemToRemove is  null)
            {
                throw new OrderItemNotFoundException($"Item: {itemCode} not found in Order: {this.Id.Value}.");
            }

            _orderItems.Remove(orderItemToRemove);
            AddEvent(new OrderItemRemoved(this, orderItemToRemove));
        }

        public virtual void UpdateItems(IEnumerable<OrderItem> orderItems)
        {
            //   var toRemove = _orderItems.ExceptBy(orderItems.Select(it => it.ItemVO.Code), oi => oi.ItemVO.Code);
            var newOrderItemsMap = orderItems.ToDictionary(k => k.ItemVO.Code);
            var currentOrderItemsMap = _orderItems.ToDictionary(k => k.ItemVO.Code);

            var added = orderItems.Where(a => !currentOrderItemsMap.ContainsKey(a.ItemVO.Code));
            var deleted = _orderItems.Where(it=> !newOrderItemsMap.ContainsKey(it.ItemVO.Code)).ToList();
            var updated = orderItems.Where(it => currentOrderItemsMap.TryGetValue(it.ItemVO.Code, out var c) && (it.Quantity != c.Quantity))
                          .Select(d => (Old: currentOrderItemsMap[d.ItemVO.Code],
                                New: d));

            foreach ( var item in added)
            {
                this.AddItem(item);
            }
            foreach (var item in deleted)
            {
                //_orderItems.Remove(item);
                this.RemoveItem(item.ItemVO.Code);
            }
            foreach (var item in updated)
            {
                item.Old.SetQuantity(item.New.Quantity);
                AddEvent(new OrderItemUpdated(this, item.Old));
            }
                                                                                                                 
        }

    }
}
