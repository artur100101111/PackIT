using PackIt.Application.Locations.Commands.Specyfications;
using PackIt.Application.Locations.Exceptions;
using PackIt.Application.Services;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;
using PackIT.Domain.Locations.Repository;

namespace PackIt.Application.Locations.Commands.ChangeLocationParent
{
    internal class ChangLocationParentHandler : ICommandHandler<ChangeLocationParent>
    {
        private ILocationRepository _locationRepository;
        private IUnitOfWork _unitOfWork;
        private ILocationReadService _locationReadService;

        public ChangLocationParentHandler(ILocationRepository locationRepository, IUnitOfWork unitOfWork, ILocationReadService locationReadService)
        {
            _locationRepository = locationRepository;
            _unitOfWork = unitOfWork;
            _locationReadService = locationReadService;
        }

        public async Task HandleAsync(ChangeLocationParent command, CancellationToken cancellationToken)
        {
            var (locationId, newParentId) = command;

            if (newParentId is long newParentIdValue)
            {
                var newParentIdExists = await _locationRepository.CheckIfExistsAsync(new GetLocationByIdSpecyfication(newParentIdValue), cancellationToken);
                if (!newParentIdExists) throw new LocationNotFoundException($"Parent Location with Id: {newParentIdValue} was not found.");

                var ancestorsOfNewParent = await _locationReadService.GetParentTreePathAsync(newParentIdValue, cancellationToken);
                                                                                                
                bool wouldCreateACycle = ancestorsOfNewParent.Any(id => id == locationId);
                if (wouldCreateACycle) throw new InvalidOperationCycleDetectedException($"Location cannot be moved to Location Id: {newParentId} due to cyle detection.");
            }


                var location = await _locationRepository.GetLocationBySpecyfictionAsync(new GetLocationByIdSpecyfication(locationId), cancellationToken);
            if (location == null) throw new LocationNotFoundException($"Location with Id: {locationId} was not found.");

            location.SetParentLocation(newParentId);

            await _locationRepository.UpdateAsync(location,cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
