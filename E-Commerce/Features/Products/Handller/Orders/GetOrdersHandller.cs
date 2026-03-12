using E_commerce.Features.Products.Quaries.Orders;
using E_commerce.Interface;
using E_Commerce.Models;
using MediatR;

namespace E_commerce.Features.Products.Handller.Orders
{
    public class GetAllOrdersHandller : IRequestHandler<GetOrderQuaries, List<Order>>
    {
        private readonly IOrderInterface _ordersRepository;

        public GetAllOrdersHandller(IOrderInterface ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<List<Order>> Handle(GetOrderQuaries request, CancellationToken cancellationToken)
        {
            return await _ordersRepository.GetAllOrders();
        }
    }
}
