using PackIt.Application.Locations.Commands.Specyfications;
using PackIt.Application.Locations.Exceptions;
using PackIt.Application.Locations.Factories;
using PackIt.Application.Services;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;
using PackIT.Domain.Locations;
using PackIT.Domain.Locations.Repository;

namespace PackIt.Application.Locations.Commands.CreateLocation
{
    internal class CreateLocationHandler : ICommandHandler<CreateLocationCommand>
    {
        private ILocationRepository _locationRepository;
        private LocationApplicationFactory _locationFactory;
        private IUnitOfWork _unitOfWork;
        private ISnowflakeIdGenerator _idGenerator;

        public CreateLocationHandler(ILocationRepository locationRepository, LocationApplicationFactory locationFactory, IUnitOfWork unitOfWork, ISnowflakeIdGenerator idGenerator)
        {
            _locationRepository = locationRepository;
            _locationFactory = locationFactory;
            _unitOfWork = unitOfWork;
            _idGenerator = idGenerator;
        }

        public async Task HandleAsync(CreateLocationCommand command, CancellationToken cancellationToken)
        {
            command.LocationId = _idGenerator.CreateId();

            var (Id, Name, Code,Description, ancestorId, LocationType) 
                =(command.LocationId.Value, command.Name,command.Code, command.Description,  command.ancesstorId, command.LocationType);

            if (ancestorId is long ancesstiorIdValue)
            {
                var ancestorExists = await _locationRepository.CheckIfExistsAsync(new GetLocationByIdSpecyfication(ancesstiorIdValue),cancellationToken);
                if (!ancestorExists)
                    throw new LocationNotFoundException($"Ancesstor with Id: {ancestorId} was not found.");
            }


            var location = await _locationFactory.CreateLocationAsync(Id, Name, Code, Description, LocationType, ancestorId ?? null, cancellationToken);

            var locationExists = await _locationRepository.CheckIfExistsAsync(new CheckIfLocationExistsByCodeSpecyfication(new LocationCode(Code)));
            if (locationExists) throw new LocationAlreadyExistsException($"Location with Code: {Code} exists aleready.");

            await _locationRepository.AddAsync(location, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
