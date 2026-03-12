using E_commerce.Features.Products.Quaries.Product;
using E_commerce.Interface.Product;
using MediatR;
using ProductModel = E_Commerce.Models.Product;

namespace E_commerce.Features.Products.Handller.Product
{
    public class GetByIDProductHandller : IRequestHandler<GetByIDProductQuaries, ProductModel>
    {
        private readonly IProductInterface _productRepository;

        public GetByIDProductHandller(IProductInterface productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductModel> Handle(GetByIDProductQuaries request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetProductById(request.Id);
        }
    }
}
