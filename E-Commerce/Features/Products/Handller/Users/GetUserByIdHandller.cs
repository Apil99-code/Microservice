using E_commerce.Features.Products.Quaries.Users;
using E_commerce.Interface;
using MediatR;
using E_Commerce.Models;

namespace E_commerce.Features.Products.Handller.Users
{
    public class GetUserHandller : IRequestHandler<GetUserByIDQuaries, User>
    {
        private readonly IUserInterface _usersRepository;

        public GetUserHandller(IUserInterface usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<User> Handle(GetUserByIDQuaries request, CancellationToken cancellationToken)
        {
            return await _usersRepository.GetUserById(request.Id);
        }
    }
}