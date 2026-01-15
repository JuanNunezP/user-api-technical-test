

public interface ICatalogRepository
{
    Task<IEnumerable<CountryDto>> GetCountriesAsync();
    Task<IEnumerable<DepartmentDto>> GetDepartmentsByCountryAsync(int countryId);
    Task<IEnumerable<CityDto>> GetCitiesByDepartmentAsync(int departmentId);
}
