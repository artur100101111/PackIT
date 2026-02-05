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


        /// <summary>
        /// Gets Location
        /// </summary>
        /// <remarks>
        /// Gets Location identified by its Id
        /// </remarks>
        /// <response code="200">Location received successfully</response>
        /// <response code="404">Location not found</response>
        [ProducesResponseType(typeof(LocationDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{LocationId:long}", Name = "GetLocationById")]
        public async Task<ActionResult<LocationDto>> Get([FromRoute] GetLocationByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }

        /// <summary>
        /// Gets Location Tree (Hierarhical structure)
        /// </summary>
        /// <remarks>
        /// Gets Location identified by its Id, with its children as a tree structure.
        /// </remarks>
        /// <response code="200">Location received successfully</response>
        /// <response code="404">Location not found</response>
        [ProducesResponseType(typeof(LocationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{LocationId:long}/tree", Name = "GetTreeLocationWithSublocationsById")]
        public async Task<ActionResult<LocationDto>> Get([FromRoute] GetLocationTreeWithSublocationsByIdQuery query, CancellationToken cancellationToken)
        {
                var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }



        /// <summary>
        /// Gets Location with its children Locations
        /// </summary>
        /// <remarks>
        /// Gets Location identified by its Id, with its children as a List structure
        /// </remarks>
        /// <response code="200">Location received successfully</response>
        /// <response code="404">Location not found</response>
        [ProducesResponseType(typeof(IEnumerable<LocationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{LocationId:long}/list", Name = "GetLocationListWithSublocationsById")]
        public async Task<ActionResult<IEnumerable<LocationDto>>> Get([FromRoute] GetLocationsListWithSublocationsByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }


        /// <summary>
        /// Gets Location by its Code (natural key)
        /// </summary>
        /// <response code="200">Lotation received successfully</response>
        /// <response code ="404">ocation not found</response>
        [ProducesResponseType(typeof(LocationDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("code/{Code}", Name = "GetLocationByCode")]
        public async Task<ActionResult<LocationDto>> Get([FromRoute] GetLocationByCodeQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);
            return OkOrNotFound(result);
        }

        /// <summary>
        /// Gets Locations by name
        /// </summary>
        /// <response code="200">Locations received successfully</response>
        /// <response code="404">Location not found</response>
        [ProducesResponseType(typeof(IEnumerable<LocationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(Name = "GetLocationsByName")]
        public async Task<ActionResult<IEnumerable<LocationDto>>> Get([FromQuery] SearchLocationByNameQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }


        /// <summary>
        /// Creates Location
        /// </summary>
        /// <remarks>
        /// Creates a new Location and returns a link to the created resource.
        /// </remarks>
        ///<respose code="201">Location Crated successfully</respose>
        ///<resposne code="400">Invalid request data</resposne>
        ///<resposne code="409">Locatin exists already</resposne> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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


        /// <summary>
        /// Updates Location
        /// </summary>
        /// <remarks>
        /// Updates Location identfied by its Id
        /// </remarks>
        ///<response code="204">Location updated successfully</response>
        ///<response code="400">Invalid request data</response> 
        ///<response code="404">Location not found</response>
        ///<response code="409">Parent Location cycle detected</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPatch("{LocationId:long}", Name = "UpdateLocation")]
        public async Task<IActionResult> Update([FromRoute] long LocationId, [FromBody] UpdateLocationCommand command, CancellationToken cancellationToken)
        {
            if (LocationId != command.LocationId)
                return BadRequest();

            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return NoContent();
        }


        /// <summary>
        /// Deletes Location
        /// </summary>
        /// <remarks>
        /// Deletes Location identified by its Id, which has no child location
        /// </remarks>
        /// <response code="204">Location deleted successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="404">Location not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{LocationId:long}", Name = "DeleteLocation")]
        public async Task<IActionResult> Delete([FromRoute] DeleteLocationCommand command, CancellationToken cancellationToken)
        {
            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return NoContent();
        }
    }
}
