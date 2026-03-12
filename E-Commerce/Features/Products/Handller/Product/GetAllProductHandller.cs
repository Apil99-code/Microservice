using E_commerce.Features.Products.Quaries.Product;
using E_commerce.Interface.Product;
using MediatR;
using ProductModel = E_Commerce.Models.Product;

namespace E_commerce.Features.Products.Handller.Product
{
    public class GetAllProductHandller : IRequestHandler<GetAllProductQuaries, List<ProductModel>>
    {
        private readonly IProductInterface _productRepository;

        public GetAllProductHandller(IProductInterface productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductModel>> Handle(GetAllProductQuaries request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllProducts();
        }
    }
}
