using AccountingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Infrastructure.SQLRepo;

public class AccountingDbContext : DbContext
{
    public AccountingDbContext(
        DbContextOptions<AccountingDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppUser> Users => Set<AppUser>();

    public DbSet<Company> Companies => Set<Company>();

    public DbSet<Account> Accounts => Set<Account>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ============================
        // APP USER
        // ============================

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.ToTable("app_users");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id");

            entity.Property(x => x.FullName)
                .HasColumnName("full_name")
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.Email)
                .HasColumnName("email")
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.PasswordHash)
                .HasColumnName("password_hash")
                .IsRequired();

            entity.Property(x => x.Role)
                .HasColumnName("role")
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(x => x.IsActive)
                .HasColumnName("is_active")
                .IsRequired();

            entity.Property(x => x.CreatedOn)
                .HasColumnName("created_on");

            entity.HasIndex(x => x.Email)
                .IsUnique();
        });


        // ============================
        // COMPANY
        // ============================

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("companies");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id");

            entity.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.Email)
                .HasColumnName("email");

            entity.Property(x => x.CreatedOn)
                .HasColumnName("created_on");
        });


        // ============================
        // ACCOUNT
        // ============================

        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("accounts");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id");

            entity.Property(x => x.CompanyId)
                .HasColumnName("company_id");

            entity.Property(x => x.Code)
                .HasColumnName("code");

            entity.Property(x => x.Name)
                .HasColumnName("name");

            entity.Property(x => x.AccountType)
                .HasColumnName("account_type");

            entity.Property(x => x.IsActive)
                .HasColumnName("is_active");

            entity.HasOne(x => x.Company)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.CompanyId);

            entity.HasIndex(x => new
            {
                x.CompanyId,
                x.Code
            }).IsUnique();
        });
    }
}