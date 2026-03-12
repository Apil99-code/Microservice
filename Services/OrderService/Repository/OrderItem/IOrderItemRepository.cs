using Dapper;
using E_commerce.Data;
using OrderItemDto = E_commerce.Dtos.OrderItems.CreateOrderItems;
using E_commerce.Interface.OrderItems;
using OrderItemModel = E_commerce.Models.OrderItem;
using Microsoft.Data.SqlClient;

namespace E_commerce.Repository.OrderItem
{
    public class IOrderItemRepository : IOrderItemsInterface
    {
        private readonly E_commerceDB _context;

        public IOrderItemRepository(E_commerceDB context)
        {
            _context = context;
        }

        public async Task<OrderItemModel> CreateOrderItem(OrderItemDto createOrderItemDtos)
        {
            using var connection = _context.GetConnection();
            await EnsureOrderExistsAsync(connection, createOrderItemDtos.OrderId);
            await EnsureProductExistsAsync(connection, createOrderItemDtos.ProductId);

            var sql = @"
                INSERT INTO dbo.OrderItems (OrderId, ProductId, Quantity, UnitPrice)
                VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice);

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int newOrderItemId;
            try
            {
                newOrderItemId = await connection.QuerySingleAsync<int>(sql, createOrderItemDtos);
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new KeyNotFoundException($"Either Order with id {createOrderItemDtos.OrderId} or Product with id {createOrderItemDtos.ProductId} was not found. Create the order and product first, then create the order item.");
            }

            return new OrderItemModel
            {
                Id = newOrderItemId,
                OrderId = createOrderItemDtos.OrderId,
                ProductId = createOrderItemDtos.ProductId,
                Quantity = createOrderItemDtos.Quantity,
                UnitPrice = createOrderItemDtos.UnitPrice
            };
        }

        private async Task EnsureOrderExistsAsync(SqlConnection connection, int orderId)
        {
            var sql = "SELECT COUNT(1) FROM dbo.Orders WHERE Id = @OrderId";
            var exists = await connection.QuerySingleAsync<int>(sql, new { OrderId = orderId });
            if (exists == 0)
                throw new KeyNotFoundException($"Order with id {orderId} was not found.");
        }

        private async Task EnsureProductExistsAsync(SqlConnection connection, int productId)
        {
            var sql = "SELECT COUNT(1) FROM dbo.Products WHERE Id = @ProductId";
            var exists = await connection.QuerySingleAsync<int>(sql, new { ProductId = productId });
            if (exists == 0)
                throw new KeyNotFoundException($"Product with id {productId} was not found.");
        }

        public async Task<bool> DeleteOrderItem(int id)
        {
            using var connection = _context.GetConnection();
            var sql = "DELETE FROM dbo.OrderItems WHERE Id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
        public async Task<List<OrderItemModel>> GetAllOrderItems()
        {
            using var connection = _context.GetConnection();
            var sql = "SELECT Id, OrderId, ProductId, Quantity, UnitPrice FROM dbo.OrderItems";
            var orderItems = await connection.QueryAsync<OrderItemModel>(sql);
            return orderItems.ToList();
        }
        public async Task<OrderItemModel> GetOrderItemById(int id)
        {
            using var connection = _context.GetConnection();
            var sql = "SELECT Id, OrderId, ProductId, Quantity, UnitPrice FROM dbo.OrderItems WHERE Id = @Id";
            var orderItem = await connection.QuerySingleOrDefaultAsync<OrderItemModel>(sql, new { Id = id });

            if (orderItem is null)
            {
                throw new KeyNotFoundException($"Order item with id {id} was not found.");
            }

            return orderItem;
        }
        public async Task<OrderItemModel> UpdateOrderItem(int id, OrderItemDto createOrderItemDtos)
        {
            using var connection = _context.GetConnection();
            await EnsureOrderExistsAsync(connection, createOrderItemDtos.OrderId);
            await EnsureProductExistsAsync(connection, createOrderItemDtos.ProductId);

            var sql = @"
                UPDATE dbo.OrderItems
                SET OrderId = @OrderId,
                    ProductId = @ProductId,
                    Quantity = @Quantity,
                    UnitPrice = @UnitPrice
                WHERE Id = @Id";

            int rowsAffected;
            try
            {
                rowsAffected = await connection.ExecuteAsync(sql, new
                {
                    Id = id,
                    createOrderItemDtos.OrderId,
                    createOrderItemDtos.ProductId,
                    createOrderItemDtos.Quantity,
                    createOrderItemDtos.UnitPrice
                });
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new KeyNotFoundException($"Either Order with id {createOrderItemDtos.OrderId} or Product with id {createOrderItemDtos.ProductId} was not found. Create the order and product first, then update the order item.");
            }

            if (rowsAffected == 0)
            {
                throw new KeyNotFoundException($"Order item with id {id} was not found.");
            }

            return await GetOrderItemById(id);
        }

    }
}