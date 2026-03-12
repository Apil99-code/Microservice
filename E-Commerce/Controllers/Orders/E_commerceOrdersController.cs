using E_commerce.Dtos.Orders;
using E_commerce.Features.Products.Command.Orders;
using E_commerce.Features.Products.Quaries.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace E_commerce.Controllers.Orders
{
    [ApiController]
    [Route("api/[controller]")]
    public class E_commerceOrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public E_commerceOrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDtos createOrderDtos)
        {
            if (!TryGetOrderDate(createOrderDtos.OrderDate, out var parsedOrderDate))
            {
                return BadRequest(new { Message = "orderDate must be a valid date. Use ISO format like 2026-03-11T10:30:00Z." });
            }

            try
            {
                var command = new CreateOrdersCommand
                {
                    UserId = createOrderDtos.UserId,
                    OrderDate = parsedOrderDate,
                    TotalAmount = createOrderDtos.TotalAmount,
                    Status = createOrderDtos.Status
                };

                await _mediator.Send(command);
                return Ok(new { Message = "Order created successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] CreateOrderRequestDtos createOrderDtos)
        {
            if (!TryGetOrderDate(createOrderDtos.OrderDate, out var parsedOrderDate))
            {
                return BadRequest(new { Message = "orderDate must be a valid date. Use ISO format like 2026-03-11T10:30:00Z." });
            }

            try
            {
                var command = new UpdateOrdersCommand
                {
                    Id = id,
                    UserId = createOrderDtos.UserId,
                    OrderDate = parsedOrderDate,
                    TotalAmount = createOrderDtos.TotalAmount,
                    Status = createOrderDtos.Status
                };

                await _mediator.Send(command);
                return Ok(new { Message = "Order updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var command = new DeleteOrdersCommand { Id = id };
                await _mediator.Send(command);
                return Ok(new { Message = "Order deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var query = new GetOrderByIDQuaries { Id = id };
                var order = await _mediator.Send(query);
                return Ok(order);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var query = new GetOrderQuaries();
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        private static bool TryGetOrderDate(string? input, out DateTime orderDate)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                orderDate = DateTime.UtcNow;
                return true;
            }

            return DateTime.TryParse(
                input,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out orderDate);
        }
    }
}
