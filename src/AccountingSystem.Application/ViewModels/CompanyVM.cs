namespace AccountingSystem.Application.ViewModels;

public class CompanyVM
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
}
