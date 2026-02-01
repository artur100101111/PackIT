using PackIt.Application.Items.Exceptions;
using PackIt.Application.ItemTypes.Commands.Spectfications;
using PackIt.Application.ItemTypes.Commands.UpdateItem;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;

namespace PackIt.Application.ItemTypes.Commands.UpdateItemType
{
    internal class UpdateItemHandler : ICommandHandler<UpdateItemTypeCommand>
    {
        private IItemTypeRepository _itemTypeRepository;
        private IUnitOfWork _unitOfWork;

        public UpdateItemHandler(IItemTypeRepository itemTypeRepository, IUnitOfWork unitOfWork)
        {
            _itemTypeRepository = itemTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(UpdateItemTypeCommand command, CancellationToken cancellationToken)
        {
            var (Id, name, code) = command;
            var itemType = await _itemTypeRepository.GetItemTypeBySpecyfictionAsync(
                                                    new GetItemTypeByIdSpecyfication(Id), cancellationToken);
            if (itemType == null) throw new ItemNotFoundException($"Item Type with Id: {Id} was not found.");

            itemType.Name = name;
            itemType.Code = code;

            await _itemTypeRepository.UpdateAsync(itemType, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
