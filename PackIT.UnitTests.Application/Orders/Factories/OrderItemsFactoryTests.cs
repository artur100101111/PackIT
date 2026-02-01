using NSubstitute;
using PackIt.Application.Items;
using PackIt.Application.Items.Commands.Specyfications;
using PackIt.Application.Items.Exceptions;
using PackIt.Application.Orders.Factories;
using PackIT.Domain.Items;
using PackIT.Domain.ItemTypes;
using PackIT.Domain.Orders.Primitives;
using Shouldly;

namespace PackIT.UnitTests.Application.Orders.Factories
{
    public class OrderItemsFactoryTests
    {

        [Fact]
        public async Task CreateOrderItemsAsync_Throws_ItemNotFoundException_When_Any_Item_Not_Found()
        {
            var orderItemPrimitives = GetOrderItemPrimitives();
            var notAllItemsFound = new List<Item>();
            notAllItemsFound.Add(new Item(1,"Item 1", "IT001", new ItemType(1,"Item Type Name", "IT001")));

            _itemRepository.GetItemsBySpecyfictionAsync(Arg.Any<GetItemsByIDListSpecyfication>()).Returns(notAllItemsFound);

            //ACT
            var exception = await Record.ExceptionAsync(()=>_orderItemFactory.CreateOrderItemsAsync(orderItemPrimitives, CancellationToken.None));

            //ASSERT

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ItemNotFoundException>();
            exception.Message.ShouldContain("2");
        }

        [Fact]
        public async Task CreateOrderItemsAsync_Returns_OrderItems_List_When_All_Items_Found()
        {
            var orderItemPrimitives = GetOrderItemPrimitives();
            var allItemsFound = new List<Item>();

            var item1Code = "IT001";
            var item2Code = "IT002";
            allItemsFound.Add(new Item(1, "Item 1", item1Code, new ItemType(1, "Item Type Name", "IT001")));
            allItemsFound.Add(new Item(2, "Item 2", item2Code, new ItemType(1, "Item Type Name", "IT001")));

            _itemRepository.GetItemsBySpecyfictionAsync(Arg.Any<GetItemsByIDListSpecyfication>()).Returns(allItemsFound);

            //ACT
            var result = await _orderItemFactory.CreateOrderItemsAsync(orderItemPrimitives,CancellationToken.None); 

            //ASSERT
            result.ShouldNotBeNull();
            result.Count.ShouldBe(orderItemPrimitives.Count);
            result.SingleOrDefault(o => o.ItemVO.Code == item1Code).ShouldNotBeNull();
            result.SingleOrDefault(o => o.ItemVO.Code == item2Code).ShouldNotBeNull();

            result.SingleOrDefault(o => o.ItemVO.Code == item1Code)!.Quantity.ShouldBe(orderItemPrimitives[0].Quantity);
            result.SingleOrDefault(o => o.ItemVO.Code == item2Code)!.Quantity.ShouldBe(orderItemPrimitives[1].Quantity);
        }

        [Fact]
        public async Task CreateOrderItemsAsync_Throws_ItemNotFoundException_When_All_Items_Not_Found()
        {
            var oiProimitives = GetOrderItemPrimitives();
            _itemRepository.GetItemsBySpecyfictionAsync(Arg.Any<GetItemsByIDListSpecyfication>(), Arg.Any<CancellationToken>())
                .Returns(Enumerable.Empty<Item>());

            //ACT
            var exception = await Record.ExceptionAsync(() => _orderItemFactory.CreateOrderItemsAsync(oiProimitives, CancellationToken.None));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ItemNotFoundException>();
            exception.Message.ShouldContain("1");
            exception.Message.ShouldContain("2");
        }

        [Fact]
        public async Task CreateOrderItemsAsync_Calls_ItemRepository_With_ItemIDs_From_Primitives()
        {
            var orderItemPrimitives = GetOrderItemPrimitives();
            var allItemsFound = new List<Item>();

            var item1Code = "IT001";
            var item2Code = "IT002";
            allItemsFound.Add(new Item(1, "Item 1", item1Code, new ItemType(1, "Item Type Name", "IT001")));
            allItemsFound.Add(new Item(2, "Item 2", item2Code, new ItemType(1, "Item Type Name", "IT001")));

            _itemRepository.GetItemsBySpecyfictionAsync(Arg.Any<GetItemsByIDListSpecyfication>()).Returns(allItemsFound);

            //ACT
            var result = await _orderItemFactory.CreateOrderItemsAsync(orderItemPrimitives, CancellationToken.None);

            //ASSERT
            await _itemRepository.Received(1)
                .GetItemsBySpecyfictionAsync(Arg.Is<GetItemsByIDListSpecyfication>(sp => sp.ItemIds.Contains(1) && sp.ItemIds.Contains(2)));
        }


        private readonly OrderItemsFactory _orderItemFactory;
        private readonly IItemRepository _itemRepository;
        public OrderItemsFactoryTests()
        {
            _itemRepository = Substitute.For<IItemRepository>();
           _orderItemFactory = new OrderItemsFactory(_itemRepository);
        }

        private List<OrderItemPrimitive> GetOrderItemPrimitives()
        {
            var oiPrimitives = new List<OrderItemPrimitive>();
            oiPrimitives.Add(new OrderItemPrimitive(1,1));
            oiPrimitives.Add(new OrderItemPrimitive(2,5));
            return oiPrimitives;
        }


    }
}
