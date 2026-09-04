using AccountingSystem.Domain.Entities;

namespace AccountingSystem.Application.Interfaces;

public interface IUserRepository
{
    Task<AppUser?> GetByEmailAsync(string email);

    Task<AppUser?> GetByIdAsync(Guid id);

    Task<List<AppUser>> GetAllAsync();

    Task<AppUser> AddAsync(AppUser user);
}