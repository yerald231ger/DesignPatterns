using PaymentMethodDescriminator.Domain.Enums;

namespace PaymentMethodDescriminator.Domain.Entities;

public class PaymentMethod
{
    public Guid Id { get; private set; }
    public PaymentMethodType Type { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; }
    public string? Description { get; private set; }

    public PaymentMethod(PaymentMethodType type, string name, string? description = null)
    {
        Id = Guid.NewGuid();
        Type = type;
        Name = name;
        Description = description;
        IsActive = true;
    }

    // Protected constructor for EF Core
    protected PaymentMethod() { }
} 