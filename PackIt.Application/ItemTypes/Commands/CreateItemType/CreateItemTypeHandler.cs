using PackIt.Application.ItemTypes.Commands.Spectfications;
using PackIt.Application.ItemTypes.Exceptions;
using PackIt.Application.Services;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;
using PackIT.Domain.ItemTypes;

namespace PackIt.Application.ItemTypes.Commands.CreateItemType
{
    internal class CreateItemTypeHandler : ICommandHandler<CreateItemTypeCommand>
    {
        private IItemTypeRepository _itemTypeRepository;
        private IUnitOfWork _unitOfWork;
        private ISnowflakeIdGenerator _idGenerator;

        public CreateItemTypeHandler(IItemTypeRepository itemTypeRepository, IUnitOfWork unitOfWork, ISnowflakeIdGenerator idGenerator)
        {
            _itemTypeRepository = itemTypeRepository;
            _unitOfWork = unitOfWork;
            _idGenerator = idGenerator;
        }
        public async Task HandleAsync(CreateItemTypeCommand command, CancellationToken cancellationToken)
        {
            command.ItemTypeId = _idGenerator.CreateId();

            var (Id, Name, TypeCode) = (command.ItemTypeId, command.Name, command.Code);

            var itemType = new ItemType(Id, Name, TypeCode);


            var itemTypeExists = await _itemTypeRepository.CheckIfItemTypeExistsAsync(new GetItemTypeByCodeSpecyfication(TypeCode), cancellationToken);
            if (itemTypeExists) throw new ItemTypeAlreadyExistsException($"Item Type with Code: {TypeCode} already exists.");

            await _itemTypeRepository.AddAsync(itemType, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
