using E_commerce.Features.Products.Quaries.Users;
using E_commerce.Interface;
using MediatR;
using E_Commerce.Models;


namespace E_commerce.Features.Products.Handller.Users
{
    public class GetAllUsersHandller : IRequestHandler<GetUserQuaries, List<User>>
    {
        private readonly IUserInterface _userService;

        public GetAllUsersHandller(IUserInterface userService)
        {
            _userService = userService;
        }

        public async Task<List<User>> Handle(GetUserQuaries request, CancellationToken cancellationToken)
        {
            return await _userService.GetAllUsers();
        }
    }
}