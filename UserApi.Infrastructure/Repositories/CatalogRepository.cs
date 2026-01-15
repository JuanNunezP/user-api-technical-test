using Microsoft.Extensions.Configuration;
using Npgsql;
using UserApi.Application.Interfaces.Repositories;

namespace UserApi.Infrastructure.Repositories;

public class CatalogRepository : ICatalogRepository
{
    private readonly string _connectionString;

    public CatalogRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("Default")!;
    }

    public async Task<IEnumerable<CountryDto>> GetCountriesAsync()
    {
        var list = new List<CountryDto>();

        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT * FROM sp_get_countries()", conn);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new CountryDto
            {
                CountryId = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }

        return list;
    }

    public async Task<IEnumerable<DepartmentDto>> GetDepartmentsByCountryAsync(int countryId)
    {
        var list = new List<DepartmentDto>();

        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT * FROM sp_get_departments_by_country(@p_countryid)", conn);
        cmd.Parameters.AddWithValue("p_countryid", countryId);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new DepartmentDto
            {
                DepartmentId = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }

        return list;
    }

    public async Task<IEnumerable<CityDto>> GetCitiesByDepartmentAsync(int departmentId)
    {
        var list = new List<CityDto>();

        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT * FROM sp_get_cities_by_department(@p_departmentid)", conn);
        cmd.Parameters.AddWithValue("p_departmentid", departmentId);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new CityDto
            {
                CityId = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }

        return list;
    }
}
