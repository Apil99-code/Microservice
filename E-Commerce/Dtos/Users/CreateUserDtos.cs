using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Dtos.users
{
    public class CreateUserDtos
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}