using Microsoft.AspNetCore.Mvc;
using PackIt.Application.Orders.Commands.AddOrderItems;
using PackIt.Application.Orders.Commands.ChangeOrderState;
using PackIt.Application.Orders.Commands.CreateOrder;
using PackIt.Application.Orders.Commands.DeleteOrder;
using PackIt.Application.Orders.DTO;
using PackIt.Application.Orders.Queries;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Queries;

namespace PackIT.API.Controllers.Orders
{
    public class OrdersController : BaseController
    {

        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        #region Queries
        public OrdersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("{OrderId:long}", Name = "GetOrderById")]
        public async Task<ActionResult<OrderDto>> Get([FromRoute] GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);
            return OkOrNotFound(result);
        }

        [HttpGet("template/{OrderId:long}", Name = "GetOrderTemplateById")]
        public async Task<ActionResult<OrderTemplateDto>> Get([FromRoute] GetOrderTemplateByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }

        [HttpGet("statuslocation",Name = "GetOrderByRequestedLocationAndStatus")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get([FromQuery] GetOrderByRequestedLocationAndStatusQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);
            return OkOrNotFound(result);
        }

        [HttpGet("location",Name = "GetOrderByRequestedLocation")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get([FromQuery] GetOrdersByRequestedLocationQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);
            return OkOrNotFound(result);
        }


        [HttpGet("template/{LocationCode}", Name = "GetOrderTemplateByLocation")]
        public async Task<ActionResult<IEnumerable<OrderTemplateDto>>> Get([FromRoute] GetOrderTemplateByLocationQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);
            return OkOrNotFound(result);
        }
        #endregion

        #region Commands
        /// <summary>
        /// show me the link
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost(Name = "CreateOrder")]
        public async Task<ActionResult> Post([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
        {
            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return CreatedAtRoute(
                routeName: "GetOrderById",
                routeValues: new { OrderId = command.OrderId },
                value: new { OrderId = command.OrderId}
                );
        }

        [HttpPost("template", Name = "CreateOrderTemplate")]
        public async Task<ActionResult> Post([FromBody] CreateOrderTemplateCommand command, CancellationToken cancellationToken)
        {
            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return CreatedAtRoute(
                routeName: "GetOrderTemplateById",
                routeValues: new { OrderId = command.OrderId },
                value: new { OrderId = command.OrderId}
                );
        }

        [HttpPut("{OrderId:long}/items", Name = "UpdateOrderItems")]
        public async Task<ActionResult> Put([FromRoute] long OrderId, [FromBody] UpdateOrderItemsCommand command, CancellationToken cancellationToken)
        {
            if (OrderId != command.OrderId)
                return BadRequest();

            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return Ok();
        }

        
        [HttpPut("template/{OrderId:long}/items", Name = "UpdateOrderTemplateItems")]
        public async Task<ActionResult> Put([FromRoute] long OrderId, [FromBody] UpdateOrderTemplateItemsCommand command, CancellationToken cancellationToken)
        {
            if (OrderId != command.OrderId)
                return BadRequest();

            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return Ok();
        }

        [HttpDelete("{OrderId:long}", Name = "DeleteOrder")]
        public async Task<ActionResult> Delete([FromRoute] DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("template/{OrderId:long}", Name = "DeleteOrderTemplate")]
        public async Task<ActionResult> Delete([FromRoute] DeleteOrderTemplateCommand command, CancellationToken cancellationToken)
        {
            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return NoContent();
        }



        [HttpPatch("{OrderId:long}/state", Name = "ChangeOrderState")]
        public async Task<ActionResult> Puti(long OrderId, [FromBody] ChangeOrderStateCommand command, CancellationToken cancellationToken)
        {
            if (OrderId != command.OrderId) return BadRequest();

            //validator can be used from FluentValidation package.
            if (command.NewOrderState == Domain.Orders.States.OrderStateEnum.Delivered && (command.DeliveryLocationId == null || command.DeliveryLocationId <= 0))
                return BadRequest("DeliveryLocationId is required when marking order as Delivered");

            await _commandDispatcher.DispatchAsync(command, cancellationToken);

            return NoContent();
        }

        #endregion

    }
}
