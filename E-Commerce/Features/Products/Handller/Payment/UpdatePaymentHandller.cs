using E_commerce.Dtos.Payment;
using E_commerce.Features.Products.Command.Payment;
using E_commerce.Interface.Payment;
using MediatR;

namespace E_commerce.Features.Products.Handller.Payment
{
    public class UpdatePaymentHandller : IRequestHandler<UpdatePaymentCommand, Unit>
    {
        private readonly IPaymentInterface _paymentRepository;
        public UpdatePaymentHandller(IPaymentInterface paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<Unit> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var updateDto = new CreatePaymentDtos
            {
                OrderId = request.OrderId,
                PaymentDate = request.PaymentDate,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod
            };

            await _paymentRepository.UpdatePayment(request.Id, updateDto);
            return Unit.Value;
        }
    }
}