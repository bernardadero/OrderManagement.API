namespace OrderManagement.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using OrderManagement.API.Models;
    using OrderManagement.API.Services;
    using Swashbuckle.AspNetCore.Annotations;


    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private static readonly List<Order> _orders = new();

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Updates the status of an existing order.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="newStatus">The new status to apply to the order.</param>
        /// <returns>The updated order if successful.</returns>
        [HttpPut("{id}/status")]
        [SwaggerOperation(Summary = "Update order status", Description = "Change the status of an order and record fulfillment if applicable.")]
        [SwaggerResponse(200, "Order updated successfully", typeof(Order))]
        [SwaggerResponse(404, "Order not found")]
        [SwaggerResponse(400, "Invalid status transition")]
        public IActionResult UpdateStatus(Guid id, [FromBody] OrderStatus newStatus)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            if (!_orderService.CanTransition(order.Status, newStatus))
                return BadRequest("Invalid transition");

            order.Status = newStatus;
            if (newStatus == OrderStatus.Delivered)
                order.FulfilledAt = DateTime.UtcNow;

            return Ok(order);
        }

        /// <summary>
        /// Returns aggregated analytics for all orders.
        /// </summary>
        /// <returns>Analytics including average order value, fulfillment time, and total orders.</returns>
        [HttpGet("analytics")]
        [SwaggerOperation(Summary = "Get order analytics", Description = "Provides statistics on order values and fulfillment times.")]
        [SwaggerResponse(200, "Returns order analytics", typeof(OrderAnalyticsDto))]
        public IActionResult GetAnalytics()
        {
            var analytics = _orderService.GetAnalytics(_orders);
            return Ok(analytics);
        }
    }
}