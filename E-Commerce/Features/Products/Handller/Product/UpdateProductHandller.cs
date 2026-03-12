using E_commerce.Dtos.Product;
using E_commerce.Features.Products.Command.Product;
using E_commerce.Interface.Product;
using MediatR;

namespace E_commerce.Features.Products.Handller.Product
{
    public class UpdateProductHandller : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductInterface _productRepository;

        public UpdateProductHandller(IProductInterface productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var updateDto = new CreateProductDtos
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock
            };

            await _productRepository.UpdateProduct(request.Id, updateDto);
            return Unit.Value;
        }
    }
}
