using MediatR;
using PaymentModel = E_Commerce.Models.Payment; 
namespace E_commerce.Features.Products.Quaries.Payment
{
   public class GetAllPaymentQuaries : IRequest<List<PaymentModel>>
    {
    }
}