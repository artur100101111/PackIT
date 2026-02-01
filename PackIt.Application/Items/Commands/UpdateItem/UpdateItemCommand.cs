using PackIt.Shared.Abstractions.Commands;

namespace PackIt.Application.Items.Commands.UpdateItem
{
    public record  UpdateItemCommand(long ItemId, string Name, string Code, long ItemTypeId): ICommand;
}
