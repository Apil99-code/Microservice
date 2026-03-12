using MediatR;

namespace E_commerce.Features.Products.Command.Orders
{
    public class DeleteOrdersCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
