using E_commerce.Dtos.Product;
using ProductModel = E_Commerce.Models.Product;

namespace E_commerce.Interface.Product
{
    public interface IProductInterface
    {
        Task<List<ProductModel>> GetAllProducts();
        Task<ProductModel> GetProductById(int id);
        Task<ProductModel> CreateProduct(CreateProductDtos product);
        Task<ProductModel> UpdateProduct(int id, CreateProductDtos product);
        Task<bool> DeleteProduct(int id);
    }
}