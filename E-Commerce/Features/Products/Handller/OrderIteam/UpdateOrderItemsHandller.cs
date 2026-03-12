using E_commerce.Dtos.OrderItems;
using E_commerce.Features.Products.Command.OrderItems;
using E_commerce.Interface.OrderItems;
using MediatR;

namespace E_commerce.Features.Products.Handller.OrderIteam
{
    public class UpdateOrderItemsHandller : IRequestHandler<UpdateOrderItemsCommand, Unit>
    {
        private readonly IOrderItemsInterface _orderItemsRepository;

        public UpdateOrderItemsHandller(IOrderItemsInterface orderItemsRepository)
        {
            _orderItemsRepository = orderItemsRepository;
        }

        public async Task<Unit> Handle(UpdateOrderItemsCommand request, CancellationToken cancellationToken)
        {
            var updateDto = new CreateOrderItems
            {
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice
            };

            await _orderItemsRepository.UpdateOrderItem(request.Id, updateDto);
            return Unit.Value;
        }
    }
}
