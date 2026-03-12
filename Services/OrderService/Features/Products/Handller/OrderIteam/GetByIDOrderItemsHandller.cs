using E_commerce.Features.Products.Quaries.OrderItems;
using E_commerce.Interface.OrderItems;
using MediatR;
using OrderItemModel = E_commerce.Models.OrderItem;

namespace E_commerce.Features.Products.Handller.OrderIteam
{
    public class GetByIDOrderItemsHandller : IRequestHandler<GetByIDOrderItemsQuaries, OrderItemModel>
    {
        private readonly IOrderItemsInterface _orderItemsRepository;

        public GetByIDOrderItemsHandller(IOrderItemsInterface orderItemsRepository)
        {
            _orderItemsRepository = orderItemsRepository;
        }

        public async Task<OrderItemModel> Handle(GetByIDOrderItemsQuaries request, CancellationToken cancellationToken)
        {
            return await _orderItemsRepository.GetOrderItemById(request.Id);
        }
    }
}
