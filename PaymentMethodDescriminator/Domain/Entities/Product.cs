using System;
using System.ComponentModel.DataAnnotations;
using PaymentMethodDescriminator.Domain.Enums;
using PaymentMethodDescriminator.Approaches.RuleBasedApproach.Rules;
using PaymentMethodDescriminator.Domain.Repositories;

namespace PaymentMethodDescriminator.Domain.Entities;

public class Product
{
    public int ProductId { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    public decimal UnitPrice { get; set; }
    
    public int CategoryId { get; set; }
    
    [Required]
    public Category Category { get; set; } = null!;
    
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    private readonly IPaymentMethodSpecification _paymentSpecification;

    // Constructor for tests compatibility
    public Product(string name, string description, decimal price, string category)
    {
        Name = name;
        Description = description;
        UnitPrice = price;
        CategoryId = 1; // We'll need to look up the actual category ID
        Category = new Category { Name = category }; // Initialize Category object
    }

    // Constructor for domain logic with specifications
    public Product(string name, string description, decimal price, Category category, IPaymentMethodSpecification paymentSpecification)
    {
        Name = name;
        Description = description;
        UnitPrice = price;
        Category = category;
        _paymentSpecification = paymentSpecification;
    }

    // Property for test compatibility
    public decimal Price => UnitPrice;

    // Domain method to check if a payment method is valid for this product
    public bool AcceptsPaymentMethod(PaymentMethod paymentMethod)
    {
        if (_paymentSpecification == null)
        {
            throw new InvalidOperationException("Payment specifications not configured for this product.");
        }

        return _paymentSpecification.CanUsePaymentMethod(this, paymentMethod);
    }

    // Factory method to create a product with standard payment rules
    public static Product CreateWithStandardRules(string name, string description, decimal price, Category category, 
        ICategoryPaymentRuleRepository repository)
    {
        var categoryRule = new ProductCategoryPaymentRule(repository);
        var priceRule = new PriceRangePaymentRule();
        var combinedRules = categoryRule.And(priceRule);

        return new Product(name, description, price, category, combinedRules);
    }

    // Factory method to create a product with custom payment rules
    public static Product CreateWithCustomRules(string name, string description, decimal price, Category category, 
        IPaymentMethodSpecification customRules)
    {
        return new Product(name, description, price, category, customRules);
    }

    // Protected constructor for EF Core
    protected Product() { }
} 