using E_Commerce.Models;
using MediatR;

namespace E_commerce.Features.Products.Quaries.Orders
{
    public class GetOrderQuaries : IRequest<List<Order>>
    {
    }
}
