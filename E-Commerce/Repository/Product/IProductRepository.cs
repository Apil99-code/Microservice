using Dapper;
using E_commerce.Data;
using E_commerce.Dtos.Product;
using IproductInterfaces = E_commerce.Interface.Product.IProductInterface;
using ProductModel = E_Commerce.Models.Product;

namespace E_commerce.Repository.Product
{
    public class IE_commerceRepository : IproductInterfaces
    {
        private readonly E_commerceDB _context;

        public IE_commerceRepository(E_commerceDB context)
        {
            _context = context;
        }

        public async Task<ProductModel> CreateProduct(CreateProductDtos createProduct)
        {
            using var connection = _context.GetConnection();
            var sql = @"
                INSERT INTO dbo.Products (Name, Description, Price, Stock)
                VALUES (@ProductName, @Description, @Price, @Stock);

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int newProductId = await connection.QuerySingleAsync<int>(sql, createProduct);
            return new ProductModel
            {
                Id = newProductId,
                ProductName = createProduct.ProductName,
                Description = createProduct.Description,
                Price = createProduct.Price,
                Stock = createProduct.Stock
            };
        }

        public async Task<bool> DeleteProduct(int id)
        {
            using var connection = _context.GetConnection();
            var sql = "DELETE FROM dbo.Products WHERE Id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }

        public async Task<List<ProductModel>> GetAllProducts()
        {
            using var connection = _context.GetConnection();
            var sql = "SELECT Id, Name AS ProductName, Description, Price, Stock FROM dbo.Products";
            var products = await connection.QueryAsync<ProductModel>(sql);
            return products.ToList();
        }

        public async Task<ProductModel> GetProductById(int id)
        {
            using var connection = _context.GetConnection();
            var sql = "SELECT Id, Name AS ProductName, Description, Price, Stock FROM dbo.Products WHERE Id = @Id";
            var product = await connection.QuerySingleOrDefaultAsync<ProductModel>(sql, new { Id = id });

            if (product is null)
            {
                throw new KeyNotFoundException($"Product with id {id} was not found.");
            }

            return product;
        }

        public async Task<ProductModel> UpdateProduct(int id, CreateProductDtos product)
        {
            using var connection = _context.GetConnection();
            var sql = @"
                UPDATE dbo.Products
                SET Name = @ProductName,
                    Description = @Description,
                    Price = @Price,
                    Stock = @Stock
                WHERE Id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new
            {
                Id = id,
                product.ProductName,
                product.Description,
                product.Price,
                product.Stock
            });

            if (affectedRows == 0)
            {
                throw new KeyNotFoundException($"Product with id {id} was not found.");
            }

            return await GetProductById(id);
        }
    }
}