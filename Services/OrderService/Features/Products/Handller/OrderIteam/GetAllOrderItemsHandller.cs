using E_commerce.Features.Products.Quaries.OrderItems;
using E_commerce.Interface.OrderItems;
using MediatR;
using OrderItemModel = E_commerce.Models.OrderItem;

namespace E_commerce.Features.Products.Handller.OrderIteam
{
    public class GetAllOrderItemsHandller : IRequestHandler<GetAllOrderItemsQuaries, List<OrderItemModel>>
    {
        private readonly IOrderItemsInterface _orderItemsRepository;

        public GetAllOrderItemsHandller(IOrderItemsInterface orderItemsRepository)
        {
            _orderItemsRepository = orderItemsRepository;
        }

        public async Task<List<OrderItemModel>> Handle(GetAllOrderItemsQuaries request, CancellationToken cancellationToken)
        {
            return await _orderItemsRepository.GetAllOrderItems();
        }
    }
}
