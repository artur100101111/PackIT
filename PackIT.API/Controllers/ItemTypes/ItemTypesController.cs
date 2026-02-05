using Microsoft.AspNetCore.Mvc;
using PackIt.Application.Items.DTO;
using PackIt.Application.ItemTypes.Commands.CreateItemType;
using PackIt.Application.ItemTypes.Commands.DeleteItemType;
using PackIt.Application.ItemTypes.Commands.UpdateItem;
using PackIt.Application.ItemTypes.Queries;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Queries;

namespace PackIT.API.Controllers.ItemType
{
    public class ItemTypesController : BaseController
    {

        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ItemTypesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        /// <summary>
        /// Gets ItemType by Id
        /// </summary>
        ///<response code="200">ItemType received sucessfully</response>
        ///<response code="404">ItemType not found</response>   //-->> 404
        [ProducesResponseType(typeof(ItemTypeDto),200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{ItemTypeId:long}", Name = "GetItemTypeById")]
        public async Task<ActionResult<ItemTypeDto>> Get([FromRoute] GetItemTypeByIdQuery query, CancellationToken cancellationToken)
        { 
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }


        /// <summary>
        /// Gets ItemType by Code (naturall key)
        /// </summary>
        ///<response code="200">ItemType received sucessfully</response>
        ///<response code="404">ItemType not found</response> 
        [ProducesResponseType(typeof(ItemTypeDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("code/{Code}", Name ="GetItemTypeByCode")]
        public async Task<ActionResult<ItemTypeDto>> Get([FromRoute] GetItemTypeByCodeQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);
            return OkOrNotFound(result);
        }


        /// <summary>
        /// Gets ItemTypes by Name
        /// </summary>
        ///<response code="200">ItemTypes received sucessfully</response>
        ///<response code="404">ItemType not found</response>   //-->> 404
        [ProducesResponseType(typeof(IEnumerable<ItemTypeDto>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(Name ="GetItemTypesByName")]
        public async Task<ActionResult<IEnumerable<ItemTypeDto>>> Get([FromQuery] SearchItemTypeByNameQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }

        /// <summary>
        /// Creates ItemType
        /// </summary>
        ///<response code="201">ItemType created sucessfully</response>
        /// <response code="400">Invalid request data</response>
        ///<reponse code="409">ItemType already exists</reponse> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost(Name="CreateItemType")]
        public async Task<ActionResult> Post([FromBody] CreateItemTypeCommand command, CancellationToken cancellationToken)
        {
            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return CreatedAtRoute
                (
                   routeName: "GetItemTypeById",
                   routeValues:  new { ItemTypeId = command.ItemTypeId },
                   value: new { ItemTypeId = command.ItemTypeId}
                );
        }

        /// <summary>
        /// Udates ItemType
        /// </summary>
        /// <remarks>
        /// Updates existing ItemTypes identified by its Id
        /// </remarks>
        ///<response code="204">IltemType created sucessfully</response>
        ///<response code="400">Invalid request data</response>   
        ///<response code="400">IltemType not found</response>   
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{ItemTypeId:long}", Name = "UpdateItemType")]
        public async Task<IActionResult> Update([FromRoute] long ItemTypeId, [FromBody] UpdateItemTypeCommand command, CancellationToken cancellationToken)
        {
            if (ItemTypeId != command.ItemId)
                return BadRequest();

            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Updates ItemType
        /// </summary>
        /// <remarks>
        /// Updates an existing ItemType identified by its Id
        /// </remarks>
        /// <reposne code="204">ItemType updated sucessfully</reposne>
        /// <reposnse code="400">Invalid request data</reposnse>
        /// <response code="400">IteType not found</response> // -->> 494
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{ItemTypeId:long}", Name ="DeleteItemType")]
        public async Task<IActionResult> Delete([FromRoute]long ItemTypeId, [FromBody] DeleteItemTypeCommand command, CancellationToken cancellationToken)
        {
            if (ItemTypeId != command.ItemTypeId)
                return BadRequest();

            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return NoContent();
        }

    }
}
