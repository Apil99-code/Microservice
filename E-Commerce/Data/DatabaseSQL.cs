using Dapper;
using Microsoft.Data.SqlClient;

namespace E_commerce.Data
{
    public class E_commerceDB
    {
        private readonly IConfiguration _configuration;

        public E_commerceDB(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            var connectionString = _configuration.GetConnectionString("ECommerceConnection")
                ?? _configuration.GetConnectionString("VideoGame")
                ?? _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string is missing. Configure ConnectionStrings:ECommerceConnection, ConnectionStrings:VideoGame, or ConnectionStrings:DefaultConnection in appsettings.json.");
            }

            return new SqlConnection(connectionString);
        }

        public async Task EnsureDatabaseObjectsAsync()
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            var sql = @"
                IF OBJECT_ID('dbo.Users', 'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.Users
                    (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Name NVARCHAR(150) NOT NULL,
                        Email NVARCHAR(255) NOT NULL,
                        Password NVARCHAR(255) NOT NULL,
                        Role NVARCHAR(100) NOT NULL
                    );
                END";

            var orderSql = @"
                IF OBJECT_ID('dbo.Orders', 'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.Orders
                    (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        UserId INT NOT NULL,
                        OrderDate DATETIME2 NOT NULL,
                        TotalAmount DECIMAL(18,2) NOT NULL,
                        Status NVARCHAR(100) NOT NULL,
                        CONSTRAINT FK_Orders_Users_UserId
                            FOREIGN KEY (UserId) REFERENCES dbo.Users(Id)
                    );
                END";

            var orderForeignKeySql = @"
                IF OBJECT_ID('dbo.Orders', 'U') IS NOT NULL
                   AND NOT EXISTS
                   (
                       SELECT 1
                       FROM sys.foreign_keys
                       WHERE name = 'FK_Orders_Users_UserId'
                         AND parent_object_id = OBJECT_ID('dbo.Orders')
                   )
                BEGIN
                    ALTER TABLE dbo.Orders
                    ADD CONSTRAINT FK_Orders_Users_UserId
                        FOREIGN KEY (UserId) REFERENCES dbo.Users(Id);
                END";

            var orderUserIdIndexSql = @"
                IF OBJECT_ID('dbo.Orders', 'U') IS NOT NULL
                   AND NOT EXISTS
                   (
                       SELECT 1
                       FROM sys.indexes
                       WHERE name = 'IX_Orders_UserId'
                         AND object_id = OBJECT_ID('dbo.Orders')
                   )
                BEGIN
                    CREATE INDEX IX_Orders_UserId ON dbo.Orders(UserId);
                END";

            var productSql = @"
                IF OBJECT_ID('dbo.Products', 'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.Products
                    (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Name NVARCHAR(150) NOT NULL,
                        Description NVARCHAR(MAX) NOT NULL,
                        Price DECIMAL(18,2) NOT NULL,
                        Stock INT NOT NULL
                    );
                END";

            var orderItemSql = @"
                IF OBJECT_ID('dbo.OrderItems', 'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.OrderItems
                    (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        OrderId INT NOT NULL,
                        ProductId INT NOT NULL,
                        Quantity INT NOT NULL,
                        UnitPrice DECIMAL(18,2) NOT NULL,
                        CONSTRAINT FK_OrderItems_Orders_OrderId
                            FOREIGN KEY (OrderId) REFERENCES dbo.Orders(Id),
                        CONSTRAINT FK_OrderItems_Products_ProductId
                            FOREIGN KEY (ProductId) REFERENCES dbo.Products(Id)
                    );
                END";

            var paymentSql = @"
                IF OBJECT_ID('dbo.Payments', 'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.Payments
                    (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        OrderId INT NOT NULL,
                        PaymentDate DATETIME2 NOT NULL,
                        Amount DECIMAL(18,2) NOT NULL,
                        PaymentMethod NVARCHAR(100) NOT NULL,
                        CONSTRAINT FK_Payments_Orders_OrderId
                            FOREIGN KEY (OrderId) REFERENCES dbo.Orders(Id)
                    );
                END";

            await connection.ExecuteAsync(sql);
            await connection.ExecuteAsync(orderSql);
            await connection.ExecuteAsync(orderForeignKeySql);
            await connection.ExecuteAsync(orderUserIdIndexSql);
            await connection.ExecuteAsync(productSql);
            await connection.ExecuteAsync(orderItemSql);
            await connection.ExecuteAsync(paymentSql);
        }
    }
}
