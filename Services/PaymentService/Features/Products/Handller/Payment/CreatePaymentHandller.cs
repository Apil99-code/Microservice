using E_commerce.Dtos.Payment;
using E_commerce.Features.Products.Command.Payment;
using E_commerce.Interface.Payment;
using MediatR;

namespace E_commerce.Features.Products.Handller.Payment
{
    public class CreatePaymentHandller : IRequestHandler<CreatePaymentCommand, Unit>
    {
        private readonly IPaymentInterface _paymentRepository;
        public CreatePaymentHandller(IPaymentInterface paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<Unit> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var createDto = new CreatePaymentDtos
            {
                OrderId = request.OrderId,
                PaymentDate = request.PaymentDate,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod
            };

            await _paymentRepository.CreatePayment(createDto);
            return Unit.Value;
        }
    }
}