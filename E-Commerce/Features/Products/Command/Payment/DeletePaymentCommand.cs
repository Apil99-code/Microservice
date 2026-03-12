using MediatR;

namespace E_commerce.Features.Products.Command.Payment
{
    public class DeletePaymentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}