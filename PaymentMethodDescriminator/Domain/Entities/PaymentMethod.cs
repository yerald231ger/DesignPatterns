using System;
using PaymentMethodDescriminator.Domain.Enums;

namespace PaymentMethodDescriminator.Domain.Entities;

public class PaymentMethod
{
    public int PaymentMethodId { get; set; }
    public string Name { get; set; }
    public string MethodType { get; set; }
    public bool IsActive { get; set; }
    public bool IsElectronicTransaction { get; set; }
    public int? Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<CategoryPaymentMethodRule> CategoryRules { get; set; }

    // Constructor for tests compatibility
    public PaymentMethod(PaymentMethodType type, string name, string? description = null)
    {
        Name = name;
        MethodType = type.ToString();
        IsActive = true;
    }

    // Property for test compatibility
    public PaymentMethodType Type => Enum.Parse<PaymentMethodType>(MethodType);

    // Protected constructor for EF Core
    protected PaymentMethod() { }
} 