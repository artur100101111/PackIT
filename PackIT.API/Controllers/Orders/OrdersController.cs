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

        /// <summary>
        /// Gets Order
        /// </summary>
        /// <remarks>
        /// Gets order identified by its Id
        /// </remarks>
        ///<response code="200">Order received successfully</response>
        ///<response code="404">Order not found</response>
        [ProducesResponseType(typeof(OrderDto),200)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{OrderId:long}", Name = "GetOrderById")]
        public async Task<ActionResult<OrderDto>> Get([FromRoute] GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);
            return OkOrNotFound(result);
        }

        /// <summary>
        /// Gets Order Template
        /// </summary>
        /// <remarks>
        /// Gets Order Template identified by its Id
        /// </remarks>
        ///<response code="200">Order Templatereceived successfully</response>
        ///<response code="404">Order Template not found</response>
        [ProducesResponseType(typeof(OrderTemplateDto), 200)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("template/{OrderId:long}", Name = "GetOrderTemplateById")]
        public async Task<ActionResult<OrderTemplateDto>> Get([FromRoute] GetOrderTemplateByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);

            return OkOrNotFound(result);
        }


        /// <summary>
        /// Gets Orders
        /// </summary>
        ///<remarks>
        /// Gets Orders identified by reqyested Location Code (Naturall key) and current OrderStatus
        /// </remarks>
        /// <response code="200">Orders received successfully</response>
        /// <resposme code="404">Orders not found</resposme>
        [ProducesResponseType(typeof(IEnumerable<OrderDto>),200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("statuslocation",Name = "GetOrdersByRequestedLocationAndStatus")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get([FromQuery] GetOrdersByRequestedLocationAndStatusQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);
            return OkOrNotFound(result);
        }

        /// <summary>
        /// Gets Orders
        /// </summary>
        /// <remarks>
        /// Gets Orders identified by requested Location Code (natural key)
        ///<return code="200">Orders received successfully</return>
        ///<return code="404">Orders not found</return>
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("location",Name = "GetOrderByRequestedLocation")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get([FromQuery] GetOrdersByRequestedLocationQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);
            return OkOrNotFound(result);
        }



        /// <summary>
        /// Gets Order Template
        /// </summary>
        /// <remarks>
        /// Gets Order Template identified by requested Location Code (naturall key)
        ///<return code="200">Orders received successfully</return>
        ///<return code="404">Orders not found</return>
        [ProducesResponseType(typeof(IEnumerable<OrderTemplateDto>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("template/{LocationCode}", Name = "GetOrderTemplateByLocation")]
        public async Task<ActionResult<IEnumerable<OrderTemplateDto>>> Get([FromRoute] GetOrderTemplateByLocationQuery query, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.QueryAsync(query, cancellationToken);
            return OkOrNotFound(result);
        }
        #endregion

        #region Commands
        /// <summary>
        /// Creates Order
        /// </summary>
        ///<returns code="201">Order created successfully</returns>
        ///<returns code="400">Invalid request data</returns>
        ///<returns code="404">Referenced Location or Item not found</returns> 
        ///<returns code="409">Order cannot be created due to a business rule conflict</returns> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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


        /// <summary>
        /// Creates Order Template
        /// </summary>
        ///<returns code="201">Order Templtate created successfully</returns>
        ///<returns code="400">Invalid request data</returns>
        ///<returns code="404">Referenced Location or Item not found</returns>
        ///<returns code="409">Order cannot be created due to a business rule conflict</returns> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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



        /// <summary>
        /// Updates Order Items
        /// </summary>
        /// <remarks>
        /// Updates Order Items by Adding, quantity subtraction or Deleteng Item from set.
        /// </remarks>
        /// <return code="200">Items set updated successfully</return>
        /// <response code="400">Invalid request data</response>
        /// <response code="404">Reference Item not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{OrderId:long}/items", Name = "UpdateOrderItems")]
        public async Task<ActionResult> Put([FromRoute] long OrderId, [FromBody] UpdateOrderItemsCommand command, CancellationToken cancellationToken)
        {
            if (OrderId != command.OrderId)
                return BadRequest();

            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return NoContent();
        }


        /// <summary>
        /// Updates Order Template Items
        /// </summary>
        /// <remarks>
        /// Updates Order Items by Adding, quantity subtraction or Deleteng Item from set.
        /// </remarks>
        /// <return code="200">Items set updated successfully</return>
        /// <response code="400">Invalid request data</response>
        /// <response code="404">Reference Item not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("template/{OrderId:long}/items", Name = "UpdateOrderTemplateItems")]
        public async Task<ActionResult> Put([FromRoute] long OrderId, [FromBody] UpdateOrderTemplateItemsCommand command, CancellationToken cancellationToken)
        {
            if (OrderId != command.OrderId)
                return BadRequest();

            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Deletes Order
        /// </summary>
        ///<remarks>
        ///Deletes Order identified by its Id
        /// </remarks>
        /// <response code="204">Order deleted successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="404">Order not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{OrderId:long}", Name = "DeleteOrder")]
        public async Task<ActionResult> Delete([FromRoute] DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Deletes Order Template
        /// </summary>
        ///<remarks>
        ///Deletes Order Template identified by its Id
        /// </remarks>
        /// <response code="204">Order Template deleted successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="404">Order Template not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("template/{OrderId:long}", Name = "DeleteOrderTemplate")]
        public async Task<ActionResult> Delete([FromRoute] DeleteOrderTemplateCommand command, CancellationToken cancellationToken)
        {
            await _commandDispatcher.DispatchAsync(command, cancellationToken);
            return NoContent();
        }


        /// <summary>
        /// Sets Order State
        /// </summary>
        ///<remarks>
        /// Sets State of Order identified by its Id
        /// </remarks>
        /// <response code="204">Order State setted sucessfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="404">Order not found</response>
        /// <response code="409">Order State cannot be changed due to domain rule</response>
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
