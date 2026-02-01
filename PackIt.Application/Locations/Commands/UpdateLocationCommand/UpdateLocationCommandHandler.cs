using PackIt.Application.Locations.Commands.Specyfications;
using PackIt.Application.Locations.Exceptions;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;
using PackIT.Domain.Locations.Repository;

namespace PackIt.Application.Locations.Commands.UpdateLocationCommand
{
    internal class UpdateLocationHandler : ICommandHandler<UpdateLocationCommand>
    {
        private ILocationRepository _locationRepository;
        private IUnitOfWork _unitOfWork;

        public UpdateLocationHandler(ILocationRepository locationRepository, IUnitOfWork unitOfWork)
        {
            _locationRepository = locationRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task HandleAsync(UpdateLocationCommand command, CancellationToken cancellationToken)
        {
            var (Id, Description) = command;

            var locaiton = await _locationRepository.GetLocationBySpecyfictionAsync(new GetLocationByIdSpecyfication(Id), cancellationToken);
            if (locaiton == null) throw new LocationNotFoundException($"Location with Id: {Id} was not found.");

            locaiton.Description = Description;

            await _locationRepository.UpdateAsync(locaiton, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

        }
    }
}
