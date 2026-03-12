using MediatR;
using E_commerce.Features.Products.Command.Users;
using E_commerce.Interface;
using E_commerce.Dtos.users;

namespace E_commerce.Features.Products.Handller.Users
{
    public class UpdateUsersHandller: IRequestHandler<UpdateUsersCommand, Unit>
    {
        private readonly IUserInterface _usersRepository;

        public UpdateUsersHandller(IUserInterface usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Unit> Handle(UpdateUsersCommand request, CancellationToken cancellationToken)
        {
             var createDto = new CreateUserDtos
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Role = request.Role
            };

            await _usersRepository.UpdateUser(request.Id, createDto);
            return Unit.Value;
        }

    }
}