using MediatR;

namespace E_commerce.Features.Products.Command.Product
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
