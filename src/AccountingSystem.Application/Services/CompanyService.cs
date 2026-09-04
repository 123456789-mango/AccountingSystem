using AccountingSystem.Application.Interfaces;
using AccountingSystem.Application.ViewModels;
using AccountingSystem.Domain.Entities;

namespace AccountingSystem.Application.Services;

public class CompanyService
{
    private readonly ICompanyRepository _repository;
    public CompanyService(ICompanyRepository repository) => _repository = repository;

    public async Task<List<CompanyVM>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        return items.Select(x => new CompanyVM
        {
            Id = x.Id,
            Name = x.Name,
            Email = x.Email
        }).ToList();
    }

    public async Task<CompanyVM> AddAsync(CompanyAddVM vm)
    {
        var company = await _repository.AddAsync(new Company
        {
            Id = Guid.NewGuid(),
            Name = vm.Name,
            Email = vm.Email
        });

        return new CompanyVM
        {
            Id = company.Id,
            Name = company.Name,
            Email = company.Email
        };
    }
}
