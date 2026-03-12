using E_commerce.Features.Products.Command.Users;
using E_commerce.Dtos.users;
using E_commerce.Interface;
using MediatR;

namespace E_commerce.Features.Products.Command.Users
{
    public class CreateUsersHandller : IRequestHandler<CreateUsersCommand, Unit>
    {
        private readonly IUserInterface _usersRepository;

        public CreateUsersHandller(IUserInterface usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Unit> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {
            var createDto = new CreateUserDtos
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Role = request.Role
            };

            await _usersRepository.CreateUser(createDto);
            return Unit.Value;
        }
    }
}