using System;

namespace PaymentMethodDescriminator.Domain.Entities;

public class CategoryPaymentMethodRule
{
    public int RuleId { get; set; }
    public int CategoryId { get; set; }
    public int PaymentMethodId { get; set; }
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public Category Category { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
} 