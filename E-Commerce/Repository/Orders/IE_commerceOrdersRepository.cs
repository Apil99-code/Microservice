using Dapper;
using E_commerce.Data;
using E_commerce.Dtos.Orders;
using E_commerce.Interface;
using E_Commerce.Models;
using Microsoft.Data.SqlClient;

namespace E_commerce.Repository.Orders
{
    public class IE_commerceOrdersRepository : IOrderInterface
    {
        private readonly E_commerceDB _context;

        public IE_commerceOrdersRepository(E_commerceDB context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrder(CreateOrderDtos createOrderDtos)
        {
            using var connection = _context.GetConnection();
            await EnsureUserExistsAsync(connection, createOrderDtos.UserId);

            var sql = @"
                INSERT INTO dbo.Orders (UserId, OrderDate, TotalAmount, Status)
                VALUES (@UserId, @OrderDate, @TotalAmount, @Status);

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int newOrderId;
            try
            {
                newOrderId = await connection.QuerySingleAsync<int>(sql, createOrderDtos);
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new KeyNotFoundException($"User with id {createOrderDtos.UserId} was not found. Create the user first, then create the order.");
            }

            return new Order
            {
                Id = newOrderId,
                UserId = createOrderDtos.UserId,
                OrderDate = createOrderDtos.OrderDate,
                TotalAmount = createOrderDtos.TotalAmount,
                Status = createOrderDtos.Status
            };
        }

        public async Task<Order> GetOrderById(int id)
        {
            using var connection = _context.GetConnection();
            var sql = "SELECT Id, UserId, OrderDate, TotalAmount, Status FROM dbo.Orders WHERE Id = @Id";
            var order = await connection.QuerySingleOrDefaultAsync<Order>(sql, new { Id = id });

            if (order is null)
            {
                throw new KeyNotFoundException($"Order with id {id} was not found.");
            }

            return order;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            using var connection = _context.GetConnection();
            var sql = "SELECT Id, UserId, OrderDate, TotalAmount, Status FROM dbo.Orders";
            var orders = await connection.QueryAsync<Order>(sql);
            return orders.ToList();
        }

        public async Task<Order> UpdateOrder(int id, CreateOrderDtos createOrderDtos)
        {
            using var connection = _context.GetConnection();
            await EnsureUserExistsAsync(connection, createOrderDtos.UserId);

            var sql = @"
                UPDATE dbo.Orders
                SET UserId = @UserId,
                    OrderDate = @OrderDate,
                    TotalAmount = @TotalAmount,
                    Status = @Status
                WHERE Id = @Id";

            int rowsAffected;
            try
            {
                rowsAffected = await connection.ExecuteAsync(sql, new
                {
                    Id = id,
                    createOrderDtos.UserId,
                    createOrderDtos.OrderDate,
                    createOrderDtos.TotalAmount,
                    createOrderDtos.Status
                });
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new KeyNotFoundException($"User with id {createOrderDtos.UserId} was not found. Create the user first, then update the order.");
            }

            if (rowsAffected == 0)
            {
                throw new KeyNotFoundException($"Order with id {id} was not found.");
            }

            return await GetOrderById(id);
        }

        private static async Task EnsureUserExistsAsync(System.Data.IDbConnection connection, int userId)
        {
            var existsSql = "SELECT COUNT(1) FROM dbo.Users WHERE Id = @UserId";
            var exists = await connection.ExecuteScalarAsync<int>(existsSql, new { UserId = userId });

            if (exists == 0)
            {
                throw new KeyNotFoundException($"User with id {userId} was not found. Create the user first, then create the order.");
            }
        }

        public async Task<bool> DeleteOrder(int id)
        {
            using var connection = _context.GetConnection();
            var sql = "DELETE FROM dbo.Orders WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
