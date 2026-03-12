using E_commerce.Features.Products.Command.Orders;
using E_commerce.Interface;
using MediatR;

namespace E_commerce.Features.Products.Handller.Orders
{
    public class DeleteOrdersHandller : IRequestHandler<DeleteOrdersCommand, Unit>
    {
        private readonly IOrderInterface _ordersRepository;

        public DeleteOrdersHandller(IOrderInterface ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<Unit> Handle(DeleteOrdersCommand request, CancellationToken cancellationToken)
        {
            await _ordersRepository.DeleteOrder(request.Id);
            return Unit.Value;
        }
    }
}
