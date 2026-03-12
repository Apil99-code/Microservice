using E_commerce.Dtos.users;
using E_Commerce.Models;

namespace E_commerce.Interface
{
    public interface IUserInterface
    {
        Task<User> CreateUser(CreateUserDtos createUserDtos);
        Task<User> GetUserById(int id);
        Task<List<User>> GetAllUsers();
        Task<User> UpdateUser(int id, CreateUserDtos createUserDtos);
        Task<bool> DeleteUser(int id);
    }
}
