namespace UserApi.Application.Commands.CreateUser;

public record CreateUserCommand(
    string FullName,
    string Phone,
    string Address,
    int CityId
);
