using E_commerce.Features.Products.Command.OrderItems;
using E_commerce.Interface.OrderItems;
using MediatR;

namespace E_commerce.Features.Products.Handller.OrderIteam
{
    public class DeleteOrderItemsHandller : IRequestHandler<DeleteOrderItemsCommand, Unit>
    {
        private readonly IOrderItemsInterface _orderItemsRepository;

        public DeleteOrderItemsHandller(IOrderItemsInterface orderItemsRepository)
        {
            _orderItemsRepository = orderItemsRepository;
        }

        public async Task<Unit> Handle(DeleteOrderItemsCommand request, CancellationToken cancellationToken)
        {
            await _orderItemsRepository.DeleteOrderItem(request.Id);
            return Unit.Value;
        }
    }
}
