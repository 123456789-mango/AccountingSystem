using AccountingSystem.Application.Interfaces;
using AccountingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Infrastructure.SQLRepo;

public class AccountRepository : IAccountRepository
{
    private readonly AccountingDbContext _context;
    public AccountRepository(AccountingDbContext context) => _context = context;

    public Task<List<Account>> GetByCompanyAsync(Guid companyId) =>
        _context.Accounts
            .Where(x => x.CompanyId == companyId)
            .OrderBy(x => x.Code)
            .ToListAsync();

    public async Task<Account> AddAsync(Account account)
    {
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        return account;
    }
}
