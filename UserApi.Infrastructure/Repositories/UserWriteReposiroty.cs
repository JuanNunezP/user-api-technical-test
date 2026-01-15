using Npgsql;
using Microsoft.Extensions.Configuration;
using UserApi.Application.Commands.CreateUser;
using UserApi.Application.Interfaces.Repositories;

namespace UserApi.Infrastructure.Repositories;

public class UserWriteRepository : IUserWriteRepository
{
    private readonly string _connectionString;

    public UserWriteRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("Default")!;
    }

    public async Task CreateAsync(CreateUserCommand cmd)
    {
        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        await using var command =
            new NpgsqlCommand("CALL sp_create_user(@n,@p,@a,@c)", conn);

        command.Parameters.AddWithValue("n", cmd.FullName);
        command.Parameters.AddWithValue("p", cmd.Phone);
        command.Parameters.AddWithValue("a", cmd.Address);
        command.Parameters.AddWithValue("c", cmd.CityId);

        await command.ExecuteNonQueryAsync();
    }
}
