using PackIt.Application.Items.Commands.ItemCreate;
using PackIt.Application.Items.Commands.Specyfications;
using PackIt.Application.Items.Exceptions;
using PackIt.Application.Items.Factories;
using PackIt.Application.ItemTypes;
using PackIt.Application.Services;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;

namespace PackIt.Application.Items.Commands.CreateItem
{
    internal sealed class CreateItemHandler : ICommandHandler<CreateItemCommand>
    {
        private IItemRepository _itemRepository;
        private IUnitOfWork _unitOfWork;
        private ItemApplicationFactory _itemFactory;
        private ISnowflakeIdGenerator _idGenerator;

        public CreateItemHandler(IItemRepository itemRepository, ItemApplicationFactory itemFactory, IItemTypeRepository itemTypeRepository, IUnitOfWork unitOfWork, ISnowflakeIdGenerator idGenerator)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
            _itemFactory = itemFactory;  
            _idGenerator = idGenerator;
        }
        public async Task HandleAsync(CreateItemCommand command, CancellationToken cancellationToken)
        {
            command.ItemId = _idGenerator.CreateId();
            var(id, name, code, itemTypeId)= (command.ItemId,command.Name,command.Code, command.ItemTypeId);

            var item = await _itemFactory.CreateItemAsync(id, name, code, itemTypeId, cancellationToken);
          
            var itemExists = await _itemRepository.CheckIfItemExistsAsync(new GetItemByCodeSpecyfication(code));
            if (itemExists) throw new ItemAlreadyExistsException($"Item with code: {item.Code} and Type Code: {item.Type.Code} already exists.");

            await _itemRepository.AddAsync(item, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
