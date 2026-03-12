using E_commerce.Dtos.Product;
using E_commerce.Features.Products.Command.Product;
using E_commerce.Interface.Product;
using MediatR;

namespace E_commerce.Features.Products.Handller.Product
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Unit>
    {
        private readonly IProductInterface _productRepository;

        public CreateProductHandler(IProductInterface productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var createDto = new CreateProductDtos
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock
            };

            await _productRepository.CreateProduct(createDto);
            return Unit.Value;
        }
    }
}