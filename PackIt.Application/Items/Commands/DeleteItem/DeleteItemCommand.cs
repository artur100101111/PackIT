using PackIt.Shared.Abstractions.Commands;

namespace PackIt.Application.Items.Commands.DeleteItem
{
    public record DeleteItemCommand(long ItemId) : ICommand;
}
