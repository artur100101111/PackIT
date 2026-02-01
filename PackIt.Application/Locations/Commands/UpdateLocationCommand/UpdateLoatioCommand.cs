using PackIt.Shared.Abstractions.Commands;

namespace PackIt.Application.Locations.Commands.UpdateLocationCommand
{
    public record UpdateLocationCommand(long LocationId,string Description) : ICommand;
}
