using MediatR;
using ProductModel = E_Commerce.Models.Product;

namespace E_commerce.Features.Products.Quaries.Product
{
    public class GetAllProductQuaries : IRequest<List<ProductModel>>
    {
    }
}