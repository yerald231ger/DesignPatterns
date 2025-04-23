using System;
using System.ComponentModel.DataAnnotations;

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

    // Constructor for tests compatibility
    public Product(string name, string description, decimal price, string category)
    {
        Name = name;
        Description = description;
        UnitPrice = price;
        CategoryId = 1; // We'll need to look up the actual category ID
        Category = new Category { Name = category }; // Initialize Category object
    }

    // Property for test compatibility
    public decimal Price => UnitPrice;

    // Protected constructor for EF Core
    protected Product() { }
} 