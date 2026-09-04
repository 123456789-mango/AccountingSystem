using AccountingSystem.Application.Interfaces;
using AccountingSystem.Application.ViewModels;
using AccountingSystem.Domain.Entities;

namespace AccountingSystem.Application.Services;

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserVM> CreateAsync(UserAddVM vm)
    {
        var existingUser =
            await _repository.GetByEmailAsync(vm.Email);

        if (existingUser != null)
        {
            throw new Exception("Email already exists.");
        }

        var user = new AppUser
        {
            Id = Guid.NewGuid(),
            FullName = vm.FullName,
            Email = vm.Email.ToLower(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(vm.Password),
            Role = vm.Role,
            IsActive = true,
            CreatedOn = DateTime.UtcNow
        };

        await _repository.AddAsync(user);

        return new UserVM
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role,
            IsActive = user.IsActive
        };
    }

    public async Task<AppUser?> ValidateLoginAsync(
        UserLoginVM vm)
    {
        var user =
            await _repository.GetByEmailAsync(vm.Email);

        if (user == null)
            return null;

        if (!user.IsActive)
            return null;

        bool isValid =
            BCrypt.Net.BCrypt.Verify(
                vm.Password,
                user.PasswordHash);

        if (!isValid)
            return null;

        return user;
    }

    public async Task<List<UserVM>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();

        return users.Select(x => new UserVM
        {
            Id = x.Id,
            FullName = x.FullName,
            Email = x.Email,
            Role = x.Role,
            IsActive = x.IsActive
        }).ToList();
    }
}