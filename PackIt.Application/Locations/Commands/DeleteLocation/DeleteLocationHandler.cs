using PackIt.Application.Locations.Commands.Specyfications;
using PackIt.Application.Locations.Exceptions;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;
using PackIT.Domain.Locations;
using PackIT.Domain.Locations.Repository;

namespace PackIt.Application.Locations.Commands.DeleteLocation
{
    internal class DeleteLocationHandler : ICommandHandler<DeleteLocationCommand>
    {
        private ILocationRepository _locationRepository;
        private IUnitOfWork _unitOfWork;

        public DeleteLocationHandler(ILocationRepository locationRepository, IUnitOfWork unitOfWork)
        {
            _locationRepository = locationRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task HandleAsync(DeleteLocationCommand command, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetLocationBySpecyfictionAsync(new GetLocationByIdSpecyfication(command.LocationId));
            if (location == null) throw new LocationNotFoundException($"Location with Id: {command.LocationId} was not found.");

            if (location.Sublocations.Count > 0) throw new LocationCannotBeDeletedException($"Location with sublocations cannot be deleted.");

            await _locationRepository.DeleteAsync(location, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken); 
        }
    }
}
