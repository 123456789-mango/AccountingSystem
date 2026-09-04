namespace AccountingSystem.Domain.Entities;

public class Company
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}
