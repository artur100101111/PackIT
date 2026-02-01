using Microsoft.AspNetCore.Mvc;
using PackIt.Application.Locations.Commands.CreateLocation;
using PackIt.Application.Locations.Commands.DeleteLocation;
using PackIt.Application.Locations.Commands.UpdateLocationCommand;
using PackIt.Application.Locations.DTO;
using PackIt.Application.Locations.Queries;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Queries;

namespace PackIT.API.Controllers.Locations
{
    public class LocationsController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public LocationsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("{LocationId:long}", Name = "GetLocationById")]
        public async Task<ActionResult<LocationDto>> Get([FromRoute] GetLocationByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }

        [HttpGet("{LocationId:long}/tree", Name = "GetTreeLocationWithSublocationsById")]
        public async Task<ActionResult<LocationDto>> Get([FromRoute] GetLocationTreeWithSublocationsByIdQuery query, CancellationToken cancellationToken)
        {
                var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }

        [HttpGet("{LocationId:long}/list", Name = "GetLocationListWithSublocationsById")]
        public async Task<ActionResult<IEnumerable<LocationDto>>> Get([FromRoute] GetLocationsListWithSublocationsByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }



        [HttpGet("code/{Code}", Name = "GetLocationByCode")]
        public async Task<ActionResult<LocationDto>> Get([FromRoute] GetLocationByCodeQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);
            return OkOrNotFound(result);
        }


        [HttpGet(Name = "GetLocationsByName")]
        public async Task<ActionResult<IEnumerable<LocationDto>>> Get([FromQuery] SearchLocationByNameQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }

        [HttpPost(Name = "CreateLocation")]
        public async Task<ActionResult> Post([FromBody] CreateLocationCommand command, CancellationToken cancellationToken)
        {
            await _commandDispatcher.DispatchAsync(command, cancellationToken);

            return CreatedAtRoute(
                routeName: "GetLocationById",
                routeValues: new { LocationId = command.LocationId },
                value: new { LocationId = command.LocationId }
                );
        }

        [HttpPatch("{LocationId:long}", Name = "UpdateLocation")]
        public async Task<IActionResult> Update([FromRoute] long LocationId, [FromBody] UpdateLocationCommand command, CancellationToken cancellationToken)
        {
            if (LocationId != command.LocationId)
                return BadRequest();

            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return NoContent();
        }


        [HttpDelete("{LocationId:long}", Name = "DeleteLocation")]
        public async Task<IActionResult> Delete([FromRoute] DeleteLocationCommand command, CancellationToken cancellationToken)
        {
            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return NoContent();
        }
    }
}
