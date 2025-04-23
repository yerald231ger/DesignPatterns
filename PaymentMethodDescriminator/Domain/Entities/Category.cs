using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentMethodDescriminator.Domain.Entities;

public class Category
{
    public int CategoryId { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public bool IsActive { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<CategoryPaymentMethodRule> PaymentMethodRules { get; set; } = new List<CategoryPaymentMethodRule>();
} 