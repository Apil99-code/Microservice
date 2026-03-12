using MediatR;
using E_Commerce.Models;

namespace E_commerce.Features.Products.Quaries.Users
{
    public class GetUserByIDQuaries : IRequest<User>
    {
        public int Id { get; set; }
    }
}