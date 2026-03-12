using E_Commerce.Models;
using MediatR;

namespace E_commerce.Features.Products.Quaries.Orders
{
    public class GetOrderByIDQuaries : IRequest<Order>
    {
        public int Id { get; set; }
    }
}
