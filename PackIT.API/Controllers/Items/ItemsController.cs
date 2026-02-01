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
    public class ItemsController: BaseController
    {

        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ItemsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("{ItemId:long}", Name = "GetItemById")]
        public async Task<ActionResult<ItemDto>> Get([FromRoute] GetItemByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }


        /// <summary>
        /// '/api/Items/code/{Code}' 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("code/{Code}", Name = "GetItemByCode")]
        public async Task<ActionResult<ItemDto>> Get([FromRoute] GetItemByCodeQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);
            return OkOrNotFound(result);
        }


        [HttpGet(Name = "GetItemsByName")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> Get([FromQuery] SearchItemsByNameQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }

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

        [HttpPut("{ItemId:long}", Name = "UpdateItem")]
        public async Task<IActionResult> Update([FromRoute] long ItemId, [FromBody] UpdateItemCommand command, CancellationToken cancellationToken)
        {
            if (ItemId != command.ItemId)
                return BadRequest();

            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return NoContent();
        }


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
