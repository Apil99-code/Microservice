using E_commerce.Dtos.Payment;
using E_commerce.Features.Products.Command.Payment;
using E_commerce.Features.Products.Quaries.Payment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers.Payment
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] RequestPaymentDtos createPaymentDtos)
        {
            try
            {
                var command = new CreatePaymentCommand
                {
                    OrderId = createPaymentDtos.OrderId,
                    Amount = createPaymentDtos.Amount,
                    PaymentMethod = createPaymentDtos.PaymentMethod,
                    PaymentDate = createPaymentDtos.PaymentDate
                };

                await _mediator.Send(command);
                return Ok(new { Message = "Payment created successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] RequestPaymentDtos updatePaymentDtos)
        {
            try
            {
                var command = new UpdatePaymentCommand
                {
                    Id = id,
                    OrderId = updatePaymentDtos.OrderId,
                    Amount = updatePaymentDtos.Amount,
                    PaymentMethod = updatePaymentDtos.PaymentMethod,
                    PaymentDate = updatePaymentDtos.PaymentDate
                };

                await _mediator.Send(command);
                return Ok(new { Message = "Payment updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                var command = new DeletePaymentCommand
                {
                    Id = id
                };

                await _mediator.Send(command);
                return Ok(new { Message = "Payment deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            try
            {
                var query = new GetByIDPaymentQuaries { Id = id };
                var payment = await _mediator.Send(query);
                return Ok(payment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var query = new GetAllPaymentQuaries();
            var payments = await _mediator.Send(query);
            return Ok(payments);
        }
    }
}