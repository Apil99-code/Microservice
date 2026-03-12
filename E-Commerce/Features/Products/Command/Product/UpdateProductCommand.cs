using MediatR;

namespace E_commerce.Features.Products.Command.Product
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}