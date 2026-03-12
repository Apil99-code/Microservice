using E_commerce.Features.Products.Quaries.Payment;
using E_commerce.Interface.Payment;
using MediatR;
using PaymentModel = E_Commerce.Models.Payment;

namespace E_commerce.Features.Products.Handller.Payment
{
    public class GetByIDPaymentHandller : IRequestHandler<GetByIDPaymentQuaries, PaymentModel>
    {
        private readonly IPaymentInterface _paymentRepository;

        public GetByIDPaymentHandller(IPaymentInterface paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentModel> Handle(GetByIDPaymentQuaries request, CancellationToken cancellationToken)
        {
            return await _paymentRepository.GetPaymentById(request.Id);
        }
    }
}