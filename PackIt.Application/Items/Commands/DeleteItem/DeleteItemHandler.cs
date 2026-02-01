using PackIt.Application.Items.Commands.Specyfications;
using PackIt.Application.Items.Exceptions;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;

namespace PackIt.Application.Items.Commands.DeleteItem
{
    internal class DeleteItemHandler : ICommandHandler<DeleteItemCommand>
    {
        private IItemRepository _itemRepository;
        private IUnitOfWork _unitOfWork;

        public DeleteItemHandler(IItemRepository itemRepository,IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task HandleAsync(DeleteItemCommand command, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.GetItemBySpecyfictionAsync(new GetItemByIdSpecyfication(command.ItemId));
            if (item == null) throw new ItemNotFoundException($"Item with Id: {command.ItemId}  was not found.");

            await _itemRepository.DeleteAsync(item, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
