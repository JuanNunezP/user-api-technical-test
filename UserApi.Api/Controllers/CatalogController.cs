using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApi.Application.Interfaces.Repositories;

namespace UserApi.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/catalog")]
public class CatalogController : ControllerBase
{
    private readonly ICatalogRepository _repo;

    public CatalogController(ICatalogRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries()
    {
        var result = await _repo.GetCountriesAsync();
        return Ok(result);
    }

    [HttpGet("departments/{countryId:int}")]
    public async Task<IActionResult> GetDepartments(int countryId)
    {
        var result = await _repo.GetDepartmentsByCountryAsync(countryId);
        return Ok(result);
    }

    [HttpGet("cities/{departmentId:int}")]
    public async Task<IActionResult> GetCities(int departmentId)
    {
        var result = await _repo.GetCitiesByDepartmentAsync(departmentId);
        return Ok(result);
    }
}
