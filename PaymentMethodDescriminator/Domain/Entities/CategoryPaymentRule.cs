using PaymentMethodDescriminator.Domain.Enums;

namespace PaymentMethodDescriminator.Domain.Entities;

public class CategoryPaymentRule
{
    public int Id { get; set; }
    public string Category { get; set; }
    public PaymentMethodType PaymentMethodType { get; set; }
    public bool IsActive { get; set; }
} 