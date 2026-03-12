using E_commerce.Features.Products.Quaries.Payment;
using E_commerce.Interface.Payment;
using MediatR;
using PaymentModel = E_Commerce.Models.Payment;

namespace E_commerce.Features.Products.Handller.Payment
{
    public class GetAllPaymentHandllers : IRequestHandler<GetAllPaymentQuaries, List<PaymentModel>>
    {
        private readonly IPaymentInterface _paymentRepository;

        public GetAllPaymentHandllers(IPaymentInterface paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<List<PaymentModel>> Handle(GetAllPaymentQuaries request, CancellationToken cancellationToken)
        {
            return await _paymentRepository.GetAllPayments();
        }
    }
}