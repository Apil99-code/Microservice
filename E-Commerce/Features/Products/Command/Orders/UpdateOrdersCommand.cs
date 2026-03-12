using MediatR;

namespace E_commerce.Features.Products.Command.Orders
{
    public class UpdateOrdersCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
