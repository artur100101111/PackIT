using Microsoft.AspNetCore.Mvc;
using PackIt.Application.Items.Commands.DeleteItem;
using PackIt.Application.Items.Commands.ItemCreate;
using PackIt.Application.Items.Commands.UpdateItem;
using PackIt.Application.Items.DTO;
using PackIt.Application.Items.Queries;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Queries;

namespace PackIT.API.Controllers.Items
{
    public class ItemsController : BaseController
    {

        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ItemsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        /// <summary>
        /// Gets Item by Id
        /// </summary>
        ///<response code="200">Item received sucessfully</response>
        ///<response code="404">Not Found</response>              
        [ProducesResponseType(typeof(ItemDto), 200)]
        [ProducesResponseType(404)]
        [HttpGet("{ItemId:long}", Name = "GetItemById")]
        public async Task<ActionResult<ItemDto>> Get([FromRoute] GetItemByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }


        /// <summary>
        /// Gets Item by Code {natural key}
        /// </summary>
        ///<response code="200">Item received sucessfully</response>
        ///<response code="404">Not Found</response>                
        [ProducesResponseType(typeof(ItemDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("code/{Code}", Name = "GetItemByCode")]
        public async Task<ActionResult<ItemDto>> Get([FromRoute] GetItemByCodeQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);
            return OkOrNotFound(result);
        }


        /// <summary>
        /// Gets Items by Name
        /// </summary>
        ///<response code="200">Items received sucessfully</response>
        ///<response code="404">Not Found</response>                     
        [ProducesResponseType(typeof(IEnumerable<ItemDto>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]                                   
        [HttpGet(Name = "GetItemsByName")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> Get([FromQuery] SearchItemsByNameQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }



        /// <summary>
        /// Creates a new item.
        /// </summary>
        /// <remarks>
        /// Creates a new item and returns a link to the created resource.
        /// </remarks>
        /// <response code="201">Item created successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="409">Item already exists</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost(Name = "CreateItem")]
        public async Task<ActionResult> Post([FromBody] CreateItemCommand command, CancellationToken cancellationToken)
        {
            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return CreatedAtRoute(
                
                routeName:"GetItemById",
                routeValues: new { ItemId = command.ItemId },
                value: new { ItemId = command.ItemId } 
                

                );
        }


        /// <summary>
        /// Updates Item.
        /// </summary>
        /// <remarks>
        /// Updates an existing Item identified by its ID
        /// </remarks>
        /// <response code="204">Item updated successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="404">Item not found</response>  
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{ItemId:long}", Name = "UpdateItem")]
        public async Task<IActionResult> Update([FromRoute] long ItemId, [FromBody] UpdateItemCommand command, CancellationToken cancellationToken)
        {
            if (ItemId != command.ItemId)
                return BadRequest();

            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return NoContent();
        }

        ///<summary>
        ///Deletes Item
        ///</summary>
        ///<remarks> 
        ///Deletes an existing Item identified by its Id.
        ///</remarks>
        ///<response code="204">Item deleted successfully</response>
        ///<response code="400">Invalid request data</response>
        ///<response code="404">Item not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{ItemId:long}", Name = "DeleteItem")]
        public async Task<IActionResult> Delete([FromRoute] long ItemId, [FromBody] DeleteItemCommand command, CancellationToken cancellationToken)
        {
            if (ItemId != command.ItemId)
                return BadRequest();

            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return NoContent();
        }
    }
}
