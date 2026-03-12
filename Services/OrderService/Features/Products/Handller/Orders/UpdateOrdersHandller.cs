using E_commerce.Dtos.Orders;
using E_commerce.Features.Products.Command.Orders;
using E_commerce.Interface;
using MediatR;

namespace E_commerce.Features.Products.Handller.Orders
{
    public class UpdateOrdersHandller : IRequestHandler<UpdateOrdersCommand, Unit>
    {
        private readonly IOrderInterface _ordersRepository;

        public UpdateOrdersHandller(IOrderInterface ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<Unit> Handle(UpdateOrdersCommand request, CancellationToken cancellationToken)
        {
            var updateDto = new CreateOrderDtos
            {
                UserId = request.UserId,
                OrderDate = request.OrderDate,
                TotalAmount = request.TotalAmount,
                Status = request.Status
            };

            await _ordersRepository.UpdateOrder(request.Id, updateDto);
            return Unit.Value;
        }
    }
}
