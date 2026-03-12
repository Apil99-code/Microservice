using OrderItemDto =E_commerce.Dtos.OrderItems.CreateOrderItems;
using OrderItemModel = E_commerce.Models.OrderItem;
namespace E_commerce.Interface.OrderItems
{
    public interface IOrderItemsInterface
    {
        Task<OrderItemModel> CreateOrderItem(OrderItemDto createOrderItemDtos);
        Task<OrderItemModel> GetOrderItemById(int id);
        Task<List<OrderItemModel>> GetAllOrderItems();
        Task<OrderItemModel> UpdateOrderItem(int id, OrderItemDto createOrderItemDtos);
        Task<bool> DeleteOrderItem(int id);
    }
}