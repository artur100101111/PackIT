using PackIt.Application.ItemTypes;
using PackIT.Domain.Items;
using PackIT.Domain.ItemTypes;
using PackIt.Application.ItemTypes.Commands.Spectfications;
using PackIt.Application.ItemTypes.Exceptions;
using PackIT.Domain.Items.Factories;

namespace PackIt.Application.Items.Factories
{
    internal class ItemApplicationFactory
    {
        private IItemTypeRepository _itemTypeRepository;
        private IItemFactory _itemDomainFactory;

        public ItemApplicationFactory(IItemTypeRepository itemTypeRepository, IItemFactory itemDomainFactory)
        {
            _itemTypeRepository = itemTypeRepository;
            _itemDomainFactory = itemDomainFactory;
        }


        public async Task<Item> CreateItemAsync(ItemId id, string itemName, string itemCode, long itemTypeId, 
              CancellationToken cancellationToken=default)
        {
            var itemType = await _itemTypeRepository.GetItemTypeBySpecyfictionAsync(new GetItemTypeByIdSpecyfication(new ItemTypeId(itemTypeId)));
            if (itemType == null) throw new ItemTypeNotFoundException($"Item type with Id: {itemTypeId} was not found.");

            var item = _itemDomainFactory.CreateItem(id, itemName, itemCode, itemType);
            return item;
        }
    }
}
