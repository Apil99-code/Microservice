using MediatR;
using E_commerce.Dtos.users;
using E_commerce.Interface;
using E_commerce.Features.Products.Command.Users;

namespace E_commerce.Features.Products.Handller.Users
{
    public class DeleteUsersHandller: IRequestHandler<DeleteUsersCommand, Unit>
    {
        private readonly IUserInterface _usersRepository;

        public DeleteUsersHandller(IUserInterface usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Unit> Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
        {
            await _usersRepository.DeleteUser(request.Id);
            return Unit.Value;
        }
    }
}