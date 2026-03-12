using MediatR;

namespace E_commerce.Features.Products.Command.OrderItems
{
    public class UpdateOrderItemsCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
