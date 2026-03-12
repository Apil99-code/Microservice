using MediatR;
using OrderItemModel = E_commerce.Models.OrderItem;

namespace E_commerce.Features.Products.Quaries.OrderItems
{
    public class GetAllOrderItemsQuaries : IRequest<List<OrderItemModel>>
    {
    }
}
