using E_commerce.Dtos.Payment;
using E_commerce.Features.Products.Command.Payment;
using E_commerce.Interface.Payment;
using MediatR;

namespace E_commerce.Features.Products.Handller.Payment
{
    public class DeletePaymentHandller : IRequestHandler<DeletePaymentCommand, Unit>
    {
        private readonly IPaymentInterface _paymentRepository;
        public DeletePaymentHandller(IPaymentInterface paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<Unit> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            await _paymentRepository.DeletePayment(request.Id);
            return Unit.Value;
        }
    }
}