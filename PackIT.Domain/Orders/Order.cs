using PackIT.Domain.Common;
using PackIT.Domain.Orders.Exceptions;
using PackIT.Domain.Orders.States;
using PackIT.Domain.Orders.ValueObjects;
using PackIT.Domain.Shared;
using System.Text.Json.Serialization;


namespace PackIT.Domain.Orders
{
    public class Order : OrderBase
    {
        private IOrderStateService? _stateService { get; set; }

        private IDateTimeService _dateService;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStateEnum State { get; private set; } = OrderStateEnum.New;
        public DateTime RequestedDeliveryTime { get; private set; }
        public DateTime? DeliveryTime { get; private set; }
        public LocationVO? DeliveryLocation { get;  private set; }

        /// <summary>
        /// It is required to save information about Order State chenges - 
        /// (instead of Domain Events which are some information that does not have to be preserved at this time and are used only for Version increment)
        /// </summary>
        private  List<OrderStateChangedEvent> _orderStateChangesHistory = new();
        public IReadOnlyCollection<OrderStateChangedEvent> StateChangesHistory => _orderStateChangesHistory.AsReadOnly();

        protected Order():base() { }
        internal Order(OrderId id, DateTime orderDate, DateTime requestedDeliveryTime, LocationVO requestedDeliveryLocation, List<OrderItem> items ):base(id, orderDate,requestedDeliveryLocation, items)
        {
            RequestedDeliveryTime = requestedDeliveryTime;
        }

        public override void AddItem(OrderItem orderItem)
        {
            if (this.State != OrderStateEnum.New)
                throw new ItemCannotBeAddedException($"Item cannot be added if Order State is: {this.State}");
            base.AddItem(orderItem);
        }

        public override void RemoveItem(string itemCode)
        {
            if(this.State != OrderStateEnum.New)
                throw new ItemCannotBeDeletedException($"Item cannot be deleted if Order State is: {this.State}");
            base.RemoveItem(itemCode);
        }

        public override void UpdateItems(IEnumerable<OrderItem> orderItems)
        {
            if (this.State != OrderStateEnum.New)
                throw new ItemCannotBeUpdatedException($"Item cannot be updated if Order State is: {this.State}");
            base.UpdateItems(orderItems);
        }

        protected void AddOrderEvent(OrderStateChangedEvent orderEvent)
        {
            base.AddEvent((IDomainEvent)orderEvent);// Add event increments version
            _orderStateChangesHistory.Add(orderEvent);
        }

        public void SetDelivery(LocationVO deliveryLocation, IDateTimeService dateTimeService)
        {
            _dateService= dateTimeService;
            this.DeliveryTime = _dateService.UtcNow;
            this.DeliveryLocation = deliveryLocation;
        }

        /// <summary>
        /// Try to change Order State. State transition check is delegated to Order State Service.
        /// </summary>
        /// <param name="newState"></param>
        public void SetState(IOrderStateService stateService,  OrderStateEnum newState,IDateTimeService dateService)
        {
            _stateService = stateService;
            _dateService = dateService;

            var currentState = State;

            EnsureStateRequirements(newState);
            this.State = _stateService.TryChangeState(this, newState);

            AddOrderEvent(new OrderStateChangedEvent(currentState, State, _dateService.UtcNow));
        }

        private void EnsureStateRequirements(OrderStateEnum newState)
        {
            if (newState == OrderStateEnum.Delivered)
            {
                if (this.DeliveryLocation == null || this.DeliveryTime == null)
                {
                    throw new DeliveryLocaitonCannotBeEmptyException($"Delivered Order Id: {Id} must have Dalivery Location");
                }
            }
        }

        public override string ToString()
        {
            return $"OrderId: {this.Id}, CreatedAt: {this.CreationDate}, RequestedDeliveryLocation: {this.RequestedDeliveryLocation}";
        }

    }
}
