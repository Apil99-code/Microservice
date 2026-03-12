using MediatR;

namespace E_commerce.Features.Products.Command.Product
{
    public class DeleteOrderCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}