using UserApi.Application.Interfaces.Repositories;

namespace UserApi.Application.Commands.CreateUser;

public class CreateUserCommandHandler
{
    private readonly IUserWriteRepository _repo;

    public CreateUserCommandHandler(IUserWriteRepository repo)
    {
        _repo = repo;
    }

    public async Task HandleAsync(CreateUserCommand cmd)
    {

        await _repo.CreateAsync(cmd);
    }

}
