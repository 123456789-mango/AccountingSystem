using AccountingSystem.Domain.Entities;

namespace AccountingSystem.Application.Interfaces;

public interface IAccountRepository
{
    Task<List<Account>> GetByCompanyAsync(Guid companyId);
    Task<Account> AddAsync(Account account);
}
