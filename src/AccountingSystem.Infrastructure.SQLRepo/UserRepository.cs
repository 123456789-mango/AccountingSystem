using AccountingSystem.Application.Interfaces;
using AccountingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Infrastructure.SQLRepo;

public class UserRepository : IUserRepository
{
    private readonly AccountingDbContext _context;

    public UserRepository(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<AppUser?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x =>
                x.Email.ToLower() == email.ToLower());
    }

    public async Task<AppUser?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<AppUser>> GetAllAsync()
    {
        return await _context.Users
            .OrderByDescending(x => x.CreatedOn)
            .ToListAsync();
    }

    public async Task<AppUser> AddAsync(AppUser user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }
}