

using PackIt.Shared.Abstractions.Commands;

namespace PackIt.Application.Locations.Commands.ChangeLocationParent
{
    public record ChangeLocationParent(long locationId, long? parentId): ICommand;
}
