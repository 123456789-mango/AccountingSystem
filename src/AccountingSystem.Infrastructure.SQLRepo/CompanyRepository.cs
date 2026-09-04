using AccountingSystem.Application.Interfaces;
using AccountingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Infrastructure.SQLRepo;

public class CompanyRepository : ICompanyRepository
{
    private readonly AccountingDbContext _context;
    public CompanyRepository(AccountingDbContext context) => _context = context;

    public Task<List<Company>> GetAllAsync() =>
        _context.Companies.OrderBy(x => x.Name).ToListAsync();

    public async Task<Company> AddAsync(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return company;
    }
}
