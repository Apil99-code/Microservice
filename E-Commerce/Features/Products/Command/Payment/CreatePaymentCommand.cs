using MediatR;

namespace E_commerce.Features.Products.Command.Payment
{
   public class CreatePaymentCommand : IRequest<Unit>
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
    }
}