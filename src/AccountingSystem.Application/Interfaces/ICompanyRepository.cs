using AccountingSystem.Domain.Entities;

namespace AccountingSystem.Application.Interfaces;

public interface ICompanyRepository
{
    Task<List<Company>> GetAllAsync();
    Task<Company> AddAsync(Company company);
}
