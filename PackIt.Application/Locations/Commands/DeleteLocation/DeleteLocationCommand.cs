

using PackIt.Shared.Abstractions.Commands;

namespace PackIt.Application.Locations.Commands.DeleteLocation
{
    public record DeleteLocationCommand(long LocationId) : ICommand;

}
