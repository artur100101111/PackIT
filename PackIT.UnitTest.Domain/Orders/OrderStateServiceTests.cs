using PackIT.Domain.Orders;
using PackIT.Domain.Orders.States;
using Shouldly;

namespace PackIT.UnitTest.Domain.Orders
{
    public class OrderStateServiceTests
    {
        private IOrderStateService _stateService;
        public OrderStateServiceTests()
        {
            _stateService = new OrderStateService();
        }

        private Order GetOrder(OrderStateEnum  state)
        {
           return  OrderFactoryHelper.CreateOrder(state);
        }

        [Fact]
        public void Try_Change_State_When_transition_is_Allowed()
        {
            //ARRANGE
            var initialState = OrderStateEnum.New;
            var order = GetOrder(initialState);

            //ACT
            var stateToSet = OrderStateEnum.InPacking;
            var result = _stateService.TryChangeState(order, stateToSet);


            //ASSERT
            Assert.Equal(stateToSet, result);
        }

       [Theory]
       [InlineData(OrderStateEnum.New, OrderStateEnum.InPacking)]
       [InlineData(OrderStateEnum.InPacking, OrderStateEnum.Packed)]
       [InlineData(OrderStateEnum.Packed, OrderStateEnum.InDelivery)]
       [InlineData(OrderStateEnum.InDelivery, OrderStateEnum.Delivered)]
       [InlineData(OrderStateEnum.New, OrderStateEnum.Canceled)]
       [InlineData(OrderStateEnum.InPacking, OrderStateEnum.Canceled)]
       [InlineData(OrderStateEnum.Packed, OrderStateEnum.Canceled)]
        public void Try_Change_State_When_transition_is_Allowed_DoesNotThrow(OrderStateEnum currentState, OrderStateEnum nextState)
        {
            //ARRANGE
            var order = GetOrder(currentState);

            //ACT
            var result = _stateService.TryChangeState(order, nextState);


            //ASSERT
            Assert.Equal(nextState, result);
        }


        [Theory]
        [InlineData(OrderStateEnum.New, OrderStateEnum.Delivered)]
        [InlineData(OrderStateEnum.InPacking, OrderStateEnum.Delivered)]
        [InlineData(OrderStateEnum.Packed, OrderStateEnum.New)]
        [InlineData(OrderStateEnum.InDelivery, OrderStateEnum.InPacking)]
        [InlineData(OrderStateEnum.Delivered, OrderStateEnum.Canceled)]
        [InlineData(OrderStateEnum.InDelivery, OrderStateEnum.Canceled)]
        public void Try_Change_State_When_transition_is_Not_Allowed_And_Throws_OrderStateIsNotAllowedException(OrderStateEnum currentState, OrderStateEnum nextState)
        {
            //ARRANGE
            var order = GetOrder(currentState);

            //ACT
            var exception = Record.Exception(()=>_stateService.TryChangeState(order, nextState));


            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<OrderStateIsNotAllowedException>();
        }

        [Fact]
        public void Try_Change_State_From_Delivered_Allways_Throws_OrderStateIsNotAllowedException()
        {
            //ARRANGE
            var order = GetOrder(OrderStateEnum.Delivered);

            //ACT
            var exception = Record.Exception(() => _stateService.TryChangeState(order, OrderStateEnum.Canceled));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<OrderStateIsNotAllowedException>();
        }

        [Fact]
        public void InitialState_Should_be_New()
        { 
            Assert.Equal(OrderStateEnum.New, _stateService.InitialState);
        }

    }
}
