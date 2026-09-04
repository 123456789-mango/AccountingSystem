using AccountingSystem.Application.Services;
using AccountingSystem.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AccountingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly CompanyService _service;
    public CompaniesController(CompanyService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Add(CompanyAddVM vm) => Ok(await _service.AddAsync(vm));
}
