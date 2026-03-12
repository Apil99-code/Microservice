using E_commerce.Dtos.OrderItems;
using E_commerce.Features.Products.Command.OrderItems;
using E_commerce.Interface.OrderItems;
using MediatR;

namespace E_commerce.Features.Products.Handller.OrderIteam
{
    public class CreateOrderItemsHandller : IRequestHandler<CreateOrderItemsCommand, Unit>
    {
        private readonly IOrderItemsInterface _orderItemsRepository;

        public CreateOrderItemsHandller(IOrderItemsInterface orderItemsRepository)
        {
            _orderItemsRepository = orderItemsRepository;
        }

        public async Task<Unit> Handle(CreateOrderItemsCommand request, CancellationToken cancellationToken)
        {
            var createDto = new CreateOrderItems
            {
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice
            };

            await _orderItemsRepository.CreateOrderItem(createDto);
            return Unit.Value;
        }
    }
}
