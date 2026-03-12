using MediatR;
using E_commerce.Dtos.Payment;
using PaymentModel= E_Commerce.Models.Payment;

namespace E_commerce.Interface.Payment
{
    public interface IPaymentInterface
    {
        Task<PaymentModel> CreatePayment(CreatePaymentDtos createPaymentDtos);
        Task<PaymentModel> GetPaymentById(int id);
        Task<List<PaymentModel>> GetAllPayments();
        Task<PaymentModel> UpdatePayment(int id, CreatePaymentDtos createPaymentDtos);
        Task<bool> DeletePayment(int id);
    }
  
}