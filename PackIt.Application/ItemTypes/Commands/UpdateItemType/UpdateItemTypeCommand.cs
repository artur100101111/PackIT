using PackIt.Shared.Abstractions.Commands;

namespace PackIt.Application.ItemTypes.Commands.UpdateItem
{
    public record UpdateItemTypeCommand(long ItemId, string Name, string Code): ICommand;
}
