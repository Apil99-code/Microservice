using MediatR;

namespace E_commerce.Features.Products.Command.Users
{
    public class DeleteUsersCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}