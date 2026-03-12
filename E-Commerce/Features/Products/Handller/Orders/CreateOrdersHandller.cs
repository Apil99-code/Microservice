using E_commerce.Dtos.Orders;
using E_commerce.Features.Products.Command.Orders;
using E_commerce.Interface;
using MediatR;

namespace E_commerce.Features.Products.Handller.Orders
{
    public class CreateOrdersHandller : IRequestHandler<CreateOrdersCommand, Unit>
    {
        private readonly IOrderInterface _ordersRepository;

        public CreateOrdersHandller(IOrderInterface ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<Unit> Handle(CreateOrdersCommand request, CancellationToken cancellationToken)
        {
            var createDto = new CreateOrderDtos
            {
                UserId = request.UserId,
                OrderDate = request.OrderDate,
                TotalAmount = request.TotalAmount,
                Status = request.Status
            };

            await _ordersRepository.CreateOrder(createDto);
            return Unit.Value;
        }
    }
}
