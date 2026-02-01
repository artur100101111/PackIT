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

        [HttpGet("{ItemTypeId:long}", Name = "GetItemTypeById")]
        public async Task<ActionResult<ItemTypeDto>> Get([FromRoute] GetItemTypeByIdQuery query, CancellationToken cancellationToken)
        { 
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }

        [HttpGet("code/{Code}", Name ="GetItemTypeByCode")]
        public async Task<ActionResult<ItemTypeDto>> Get([FromRoute] GetItemTypeByCodeQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);
            return OkOrNotFound(result);
        }


        [HttpGet(Name ="GetItemTypesByName")]
        public async Task<ActionResult<IEnumerable<ItemTypeDto>>> Get([FromQuery] SearchItemTypeByNameQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }

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

        [HttpPut("{ItemTypeId:long}", Name = "UpdateItemType")]
        public async Task<IActionResult> Update([FromRoute] long ItemTypeId, [FromBody] UpdateItemTypeCommand command, CancellationToken cancellationToken)
        {
            if (ItemTypeId != command.ItemId)
                return BadRequest();

            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return NoContent();
        }


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
