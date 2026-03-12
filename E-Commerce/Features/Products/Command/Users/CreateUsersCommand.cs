using MediatR;
using E_commerce.Dtos.users;


namespace E_commerce.Features.Products.Command.Users
{

    public class CreateUsersCommand :  IRequest<Unit>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}