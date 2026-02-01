using PackIt.Application.Items.Commands.Specyfications;
using PackIt.Application.Items.Exceptions;
using PackIt.Application.ItemTypes;
using PackIt.Application.ItemTypes.Commands.Spectfications;
using PackIt.Application.ItemTypes.Exceptions;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;

namespace PackIt.Application.Items.Commands.UpdateItem
{
    internal class UpdateItemHandler : ICommandHandler<UpdateItemCommand>
    {
        private IItemRepository _itemRepository;
        private IItemTypeRepository _itemTypeRepository;
        private IUnitOfWork _unitOfWork;

        public UpdateItemHandler(IItemRepository itemRepository, IItemTypeRepository itemTypeRepository, IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _itemTypeRepository = itemTypeRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task HandleAsync(UpdateItemCommand command, CancellationToken cancellationToken)
        {
            var (Id, name, code, typeId) = command;
            var item = await _itemRepository.GetItemBySpecyfictionAsync(new GetItemByIdSpecyfication(Id), cancellationToken);
            if (item == null) throw new ItemNotFoundException($"Item with Id: {Id} was not found.");

            if (item!= null && item.TypeID != typeId)
            {
                var itemTypeExists = await _itemTypeRepository.CheckIfItemTypeExistsAsync(new GetItemTypeByIdSpecyfication(typeId), cancellationToken);
                if (!itemTypeExists) throw new ItemTypeNotFoundException($"Item Type with Id: {typeId} was not found.");
            }

            item.Name = name;
            item.Code = code;
            item.TypeID = typeId;

            await _itemRepository.UpdateAsync(item,cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
