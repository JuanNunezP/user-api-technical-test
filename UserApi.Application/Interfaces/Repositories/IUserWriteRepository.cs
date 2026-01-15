using UserApi.Application.Commands.CreateUser;

namespace UserApi.Application.Interfaces.Repositories;

public interface IUserWriteRepository
{
    Task CreateAsync(CreateUserCommand command);
}
