using E_commerce.Data;
using E_commerce.Dtos.users;
using E_commerce.Interface;
using Dapper;
using E_Commerce.Models;

namespace E_commerce.Repository.Users
{
    public class IE_commerceUsersRepository : IUserInterface
    {
        private readonly E_commerceDB _context;

        public IE_commerceUsersRepository(E_commerceDB context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(CreateUserDtos createUserDtos)
        {
            using var connection = _context.GetConnection();
            var sql = @"
                INSERT INTO dbo.Users (Name, Email, Password, Role)
                VALUES (@Name, @Email, @Password, @Role);

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            var newUserId = await connection.QuerySingleAsync<int>(sql, createUserDtos);

            return new User
            {
                Id = newUserId,
                Name = createUserDtos.Name,
                Email = createUserDtos.Email,
                Password = createUserDtos.Password,
                Role = createUserDtos.Role
            };
        }

        public async Task<User> GetUserById(int id)
        {
            using var connection = _context.GetConnection();
            var sql = "SELECT Id, Name, Email, Password, Role FROM dbo.Users WHERE Id = @Id";
            var user = await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });

            if (user is null)
            {
                throw new KeyNotFoundException($"User with id {id} was not found.");
            }

            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            using var connection = _context.GetConnection();
            var sql = "SELECT Id, Name, Email, Password, Role FROM dbo.Users";
            var users = await connection.QueryAsync<User>(sql);
            return users.ToList();
        }

        public async Task<bool> DeleteUser(int id)
        {
            using var connection = _context.GetConnection();
            var sql = "DELETE FROM dbo.Users WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<User> UpdateUser(int id, CreateUserDtos createUserDtos)
        {
            using var connection = _context.GetConnection();
            var sql = @"
                UPDATE dbo.Users
                SET Name = @Name, Email = @Email, Password = @Password, Role = @Role
                WHERE Id = @Id";

            await connection.ExecuteAsync(sql, new
            {
                Id = id,
                Name = createUserDtos.Name,
                Email = createUserDtos.Email,
                Password = createUserDtos.Password,
                Role = createUserDtos.Role
            });

            return new User
            {
                Id = id,
                Name = createUserDtos.Name,
                Email = createUserDtos.Email,
                Password = createUserDtos.Password,
                Role = createUserDtos.Role
            };
        }
    }
}



