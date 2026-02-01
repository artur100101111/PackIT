using PackIt.Shared.Abstractions.Commands;

namespace PackIt.Application.Items.Commands.ItemCreate
{
    public record CreateItemCommand : ICommand
    {
        public CreateItemCommand(long? itemId, string name, string code, long itemTypeId)
        {
            ItemId = itemId;
            Name = name;
            Code = code;
            ItemTypeId = itemTypeId;
        }

        public long? ItemId { get; set; }
        public string Name { get; init; }
        public string Code { get; init; }
        public long ItemTypeId { get; init; }


    }
}
