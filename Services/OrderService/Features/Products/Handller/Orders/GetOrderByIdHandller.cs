using E_commerce.Features.Products.Quaries.Orders;
using E_commerce.Interface;
using E_Commerce.Models;
using MediatR;

namespace E_commerce.Features.Products.Handller.Orders
{
    public class GetOrderHandller : IRequestHandler<GetOrderByIDQuaries, Order>
    {
        private readonly IOrderInterface _ordersRepository;

        public GetOrderHandller(IOrderInterface ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<Order> Handle(GetOrderByIDQuaries request, CancellationToken cancellationToken)
        {
            return await _ordersRepository.GetOrderById(request.Id);
        }
    }
}
