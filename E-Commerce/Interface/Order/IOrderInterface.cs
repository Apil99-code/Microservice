using E_commerce.Dtos.Orders;
using E_Commerce.Models;

namespace E_commerce.Interface
{
    public interface IOrderInterface
    {
        Task<Order> CreateOrder(CreateOrderDtos createOrderDtos);
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetAllOrders();
        Task<Order> UpdateOrder(int id, CreateOrderDtos createOrderDtos);
        Task<bool> DeleteOrder(int id);
    }
}
