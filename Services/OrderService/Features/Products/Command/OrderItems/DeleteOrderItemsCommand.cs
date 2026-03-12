using MediatR;

namespace E_commerce.Features.Products.Command.OrderItems
{
    public class DeleteOrderItemsCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
