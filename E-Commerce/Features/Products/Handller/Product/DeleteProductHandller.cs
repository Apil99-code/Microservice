using E_commerce.Features.Products.Command.Product;
using E_commerce.Interface.Product;
using MediatR;

namespace E_commerce.Features.Products.Handller.Product
{
    public class DeleteProductHandller : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductInterface _productRepository;

        public DeleteProductHandller(IProductInterface productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _productRepository.DeleteProduct(request.Id);
            return Unit.Value;
        }
    }
}
