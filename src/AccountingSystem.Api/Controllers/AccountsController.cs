using AccountingSystem.Application.Services;
using AccountingSystem.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AccountingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly AccountService _service;
    public AccountsController(AccountService service) => _service = service;

    [HttpGet("{companyId:guid}")]
    public async Task<IActionResult> GetByCompany(Guid companyId) =>
        Ok(await _service.GetByCompanyAsync(companyId));

    [HttpPost]
    public async Task<IActionResult> Add(AccountAddVM vm) =>
        Ok(await _service.AddAsync(vm));
}
