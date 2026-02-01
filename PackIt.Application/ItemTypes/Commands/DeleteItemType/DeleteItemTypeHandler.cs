using PackIt.Application.ItemTypes.Commands.Spectfications;
using PackIt.Application.ItemTypes.Exceptions;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;
using PackIT.Domain.ItemTypes;

namespace PackIt.Application.ItemTypes.Commands.DeleteItemType
{
    internal class DeleteItemTypeHandler : ICommandHandler<DeleteItemTypeCommand>
    {
        private IItemTypeRepository _itemTypeRepository;
        private IUnitOfWork _unitOfWOrk;

        public DeleteItemTypeHandler(IItemTypeRepository itemTypeRepository, IUnitOfWork unitOfWork)
        {
            _itemTypeRepository = itemTypeRepository;
            _unitOfWOrk = unitOfWork;
        }

        public async Task HandleAsync(DeleteItemTypeCommand command, CancellationToken cancellationToken)
        {
            var itemType =await _itemTypeRepository.GetItemTypeBySpecyfictionAsync(new GetItemTypeByIdSpecyfication(command.ItemTypeId));
            if (itemType == null) throw new ItemTypeNotFoundException($"Item Type with Id: {command.ItemTypeId} was not foud.");

            await _itemTypeRepository.DeleteAsync(itemType, cancellationToken);
            await _unitOfWOrk.SaveAsync(cancellationToken);
        }
    }
}
