using Dapper;
using E_commerce.Data;
using E_commerce.Dtos.Payment;
using E_commerce.Interface.Payment;
using Microsoft.Data.SqlClient;
using PaymentModel = E_Commerce.Models.Payment;

namespace E_commerce.Repository.Payment
{
    public class IPaymentRepository : IPaymentInterface
    {
        private readonly E_commerceDB _context;

        public IPaymentRepository(E_commerceDB context)
        {
            _context = context;
        }

        public async Task<PaymentModel> CreatePayment(CreatePaymentDtos createPaymentDtos)
        {
            using var connection = _context.GetConnection();
            await EnsureOrderExistsAsync(connection, createPaymentDtos.OrderId);

            var sql = @"
                INSERT INTO dbo.Payments (OrderId, PaymentDate, Amount, PaymentMethod)
                VALUES (@OrderId, @PaymentDate, @Amount, @PaymentMethod);

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int newPaymentId;
            try
            {
                newPaymentId = await connection.QuerySingleAsync<int>(sql, createPaymentDtos);
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new KeyNotFoundException($"Order with id {createPaymentDtos.OrderId} was not found.");
            }

            return new PaymentModel
            {
                Id = newPaymentId,
                OrderId = createPaymentDtos.OrderId,
                PaymentDate = createPaymentDtos.PaymentDate,
                Amount = createPaymentDtos.Amount,
                PaymentMethod = createPaymentDtos.PaymentMethod
            };
        }

        public async Task<PaymentModel> GetPaymentById(int id)
        {
            using var connection = _context.GetConnection();
            var sql = "SELECT Id, OrderId, PaymentDate, Amount, PaymentMethod FROM dbo.Payments WHERE Id = @Id";
            var payment = await connection.QuerySingleOrDefaultAsync<PaymentModel>(sql, new { Id = id });

            if (payment is null)
            {
                throw new KeyNotFoundException($"Payment with id {id} was not found.");
            }

            return payment;
        }

        public async Task<List<PaymentModel>> GetAllPayments()
        {
            using var connection = _context.GetConnection();
            var sql = "SELECT Id, OrderId, PaymentDate, Amount, PaymentMethod FROM dbo.Payments";
            var payments = await connection.QueryAsync<PaymentModel>(sql);
            return payments.ToList();
        }

        public async Task<PaymentModel> UpdatePayment(int id, CreatePaymentDtos createPaymentDtos)
        {
            using var connection = _context.GetConnection();
            await EnsureOrderExistsAsync(connection, createPaymentDtos.OrderId);

            var sql = @"
                UPDATE dbo.Payments
                SET OrderId = @OrderId,
                    PaymentDate = @PaymentDate,
                    Amount = @Amount,
                    PaymentMethod = @PaymentMethod
                WHERE Id = @Id";

            int rowsAffected;
            try
            {
                rowsAffected = await connection.ExecuteAsync(sql, new
                {
                    Id = id,
                    createPaymentDtos.OrderId,
                    createPaymentDtos.PaymentDate,
                    createPaymentDtos.Amount,
                    createPaymentDtos.PaymentMethod
                });
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new KeyNotFoundException($"Order with id {createPaymentDtos.OrderId} was not found.");
            }

            if (rowsAffected == 0)
            {
                throw new KeyNotFoundException($"Payment with id {id} was not found.");
            }

            return await GetPaymentById(id);
        }

        public async Task<bool> DeletePayment(int id)
        {
            using var connection = _context.GetConnection();
            var sql = "DELETE FROM dbo.Payments WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

        private static async Task EnsureOrderExistsAsync(SqlConnection connection, int orderId)
        {
            var sql = "SELECT COUNT(1) FROM dbo.Orders WHERE Id = @OrderId";
            var exists = await connection.QuerySingleAsync<int>(sql, new { OrderId = orderId });
            if (exists == 0)
            {
                throw new KeyNotFoundException($"Order with id {orderId} was not found.");
            }
        }
    }
}