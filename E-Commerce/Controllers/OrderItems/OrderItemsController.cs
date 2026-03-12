using E_commerce.Dtos.OrderItems;
using E_commerce.Features.Products.Command.OrderItems;
using E_commerce.Features.Products.Quaries.OrderItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers.OrderItems
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem([FromBody] CreateOrderItems createOrderItemDtos)
        {
            try
            {
                var command = new CreateOrderItemsCommand
                {
                    OrderId = createOrderItemDtos.OrderId,
                    ProductId = createOrderItemDtos.ProductId,
                    Quantity = createOrderItemDtos.Quantity,
                    UnitPrice = createOrderItemDtos.UnitPrice
                };

                await _mediator.Send(command);
                return Ok(new { Message = "Order item created successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] CreateOrderItems updateOrderItemDtos)
        {
            try
            {
                var command = new UpdateOrderItemsCommand
                {
                    Id = id,
                    OrderId = updateOrderItemDtos.OrderId,
                    ProductId = updateOrderItemDtos.ProductId,
                    Quantity = updateOrderItemDtos.Quantity,
                    UnitPrice = updateOrderItemDtos.UnitPrice
                };

                await _mediator.Send(command);
                return Ok(new { Message = "Order item updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var command = new DeleteOrderItemsCommand { Id = id };
            await _mediator.Send(command);
            return Ok(new { Message = "Order item deleted successfully." });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderItemById(int id)
        {
            try
            {
                var query = new GetByIDOrderItemsQuaries { Id = id };
                var orderItem = await _mediator.Send(query);
                return Ok(orderItem);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems()
        {
            var query = new GetAllOrderItemsQuaries();
            var orderItems = await _mediator.Send(query);
            return Ok(orderItems);
        }
    }
}
