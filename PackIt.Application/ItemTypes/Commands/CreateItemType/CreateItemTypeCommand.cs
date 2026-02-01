using PackIt.Shared.Abstractions.Commands;
using PackIT.Domain.ItemTypes;

namespace PackIt.Application.ItemTypes.Commands.CreateItemType
{
    public record CreateItemTypeCommand : ICommand
    {
        public long? ItemTypeId { get; set; }
        public string Name { get; init; }
        public string Code { get; init; }

        public CreateItemTypeCommand(long? ItemTypeId, string Name, string Code)
        {
            this.ItemTypeId = ItemTypeId;
            this.Name = Name;
            this.Code = Code;
        }
    }

}
