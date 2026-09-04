using AccountingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Infrastructure.SQLRepo;

public class AccountingDbContext : DbContext
{
    public AccountingDbContext(DbContextOptions<AccountingDbContext> options) : base(options) { }

    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Account> Accounts => Set<Account>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>().ToTable("app_users");
        modelBuilder.Entity<Company>().ToTable("companies");
        modelBuilder.Entity<Account>().ToTable("accounts");

        modelBuilder.Entity<AppUser>().HasIndex(x => x.Email).IsUnique();

        modelBuilder.Entity<Account>()
            .HasOne(x => x.Company)
            .WithMany(x => x.Accounts)
            .HasForeignKey(x => x.CompanyId);

        modelBuilder.Entity<Account>()
            .HasIndex(x => new { x.CompanyId, x.Code })
            .IsUnique();
    }
}
