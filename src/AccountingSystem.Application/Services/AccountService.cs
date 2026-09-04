using AccountingSystem.Application.Interfaces;
using AccountingSystem.Application.ViewModels;
using AccountingSystem.Domain.Entities;

namespace AccountingSystem.Application.Services;

public class AccountService
{
    private readonly IAccountRepository _repository;
    public AccountService(IAccountRepository repository) => _repository = repository;

    public async Task<List<AccountVM>> GetByCompanyAsync(Guid companyId)
    {
        var items = await _repository.GetByCompanyAsync(companyId);
        return items.Select(x => new AccountVM
        {
            Id = x.Id,
            Code = x.Code,
            Name = x.Name,
            AccountType = x.AccountType
        }).ToList();
    }

    public async Task<AccountVM> AddAsync(AccountAddVM vm)
    {
        var account = await _repository.AddAsync(new Account
        {
            Id = Guid.NewGuid(),
            CompanyId = vm.CompanyId,
            Code = vm.Code,
            Name = vm.Name,
            AccountType = vm.AccountType
        });

        return new AccountVM
        {
            Id = account.Id,
            Code = account.Code,
            Name = account.Name,
            AccountType = account.AccountType
        };
    }
}
