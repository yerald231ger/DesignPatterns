using Moq;
using PaymentMethodDescriminator.Approaches.RuleBasedApproach.Rules;
using PaymentMethodDescriminator.Approaches.RuleBasedApproach.Services;
using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Enums;
using PaymentMethodDescriminator.Domain.Repositories;
using Xunit;

namespace PaymentMethodDescriminator.Tests;

public class RuleBasedApproachTests
{
    private readonly Mock<ICategoryPaymentRuleRepository> _mockRepository;
    private readonly Category _foodCategory;
    private readonly Category _electronicsCategory;

    public RuleBasedApproachTests()
    {
        _foodCategory = new Category { CategoryId = 1, Name = "Food", IsActive = true };
        _electronicsCategory = new Category { CategoryId = 2, Name = "Electronics", IsActive = true };

        _mockRepository = new Mock<ICategoryPaymentRuleRepository>();
        _mockRepository.Setup(r => r.GetAllActiveRulesAsync())
            .ReturnsAsync(new List<CategoryPaymentMethodRule>
            {
                new CategoryPaymentMethodRule 
                { 
                    RuleId = 1, 
                    Category = _foodCategory,
                    PaymentMethod = new PaymentMethod(PaymentMethodType.Cash, "Cash")
                },
                new CategoryPaymentMethodRule 
                { 
                    RuleId = 2, 
                    Category = _foodCategory,
                    PaymentMethod = new PaymentMethod(PaymentMethodType.SodexoVoucher, "Sodexo")
                },
                new CategoryPaymentMethodRule 
                { 
                    RuleId = 3, 
                    Category = _electronicsCategory,
                    PaymentMethod = new PaymentMethod(PaymentMethodType.CreditCard, "Credit Card")
                },
                new CategoryPaymentMethodRule 
                { 
                    RuleId = 4, 
                    Category = _electronicsCategory,
                    PaymentMethod = new PaymentMethod(PaymentMethodType.DebitCard, "Debit Card")
                }
            });
    }

    [Fact]
    public async Task ValidatePaymentMethod_WhenProductPriceIsWithinRange_ReturnsTrue()
    {
        // Arrange
        var rules = new IPaymentMethodRule[] { new PriceRangePaymentRule() };
        var service = new RuleBasedPaymentService(rules);
        var product = new Product("Test Product", "Description", 500m, "Electronics") { Category = _electronicsCategory };
        var paymentMethod = new PaymentMethod(PaymentMethodType.Cash, "Cash");

        // Act
        var result = await service.ValidatePaymentMethodAsync(product, paymentMethod);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ValidatePaymentMethod_WhenProductPriceExceedsRange_ReturnsFalse()
    {
        // Arrange
        var rules = new IPaymentMethodRule[] { new PriceRangePaymentRule() };
        var service = new RuleBasedPaymentService(rules);
        var product = new Product("Test Product", "Description", 1500m, "Electronics") { Category = _electronicsCategory };
        var paymentMethod = new PaymentMethod(PaymentMethodType.Cash, "Cash");

        // Act
        var result = await service.ValidatePaymentMethodAsync(product, paymentMethod);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidatePaymentMethod_WhenCategoryAllowsPaymentMethod_ReturnsTrue()
    {
        // Arrange
        var rules = new IPaymentMethodRule[] { new ProductCategoryPaymentRule(_mockRepository.Object) };
        var service = new RuleBasedPaymentService(rules);
        var product = new Product("Test Product", "Description", 100m, "Food") { Category = _foodCategory };
        var paymentMethod = new PaymentMethod(PaymentMethodType.Cash, "Cash");

        // Act
        var result = await service.ValidatePaymentMethodAsync(product, paymentMethod);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ValidatePaymentMethod_WhenCategoryDoesNotAllowPaymentMethod_ReturnsFalse()
    {
        // Arrange
        var rules = new IPaymentMethodRule[] { new ProductCategoryPaymentRule(_mockRepository.Object) };
        var service = new RuleBasedPaymentService(rules);
        var product = new Product("Test Product", "Description", 100m, "Food") { Category = _foodCategory };
        var paymentMethod = new PaymentMethod(PaymentMethodType.DebitCard, "Debit Card");

        // Act
        var result = await service.ValidatePaymentMethodAsync(product, paymentMethod);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidatePaymentMethod_WhenAllRulesPass_ReturnsTrue()
    {
        // Arrange
        var rules = new IPaymentMethodRule[] 
        { 
            new PriceRangePaymentRule(),
            new ProductCategoryPaymentRule(_mockRepository.Object)
        };
        var service = new RuleBasedPaymentService(rules);
        var product = new Product("Test Product", "Description", 500m, "Food") { Category = _foodCategory };
        var paymentMethod = new PaymentMethod(PaymentMethodType.Cash, "Cash");

        // Act
        var result = await service.ValidatePaymentMethodAsync(product, paymentMethod);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ValidatePaymentMethod_WhenAnyRuleFails_ReturnsFalse()
    {
        // Arrange
        var rules = new IPaymentMethodRule[] 
        { 
            new PriceRangePaymentRule(),
            new ProductCategoryPaymentRule(_mockRepository.Object)
        };
        var service = new RuleBasedPaymentService(rules);
        var product = new Product("Test Product", "Description", 1500m, "Food") { Category = _foodCategory };
        var paymentMethod = new PaymentMethod(PaymentMethodType.Cash, "Cash");

        // Act
        var result = await service.ValidatePaymentMethodAsync(product, paymentMethod);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidatePaymentMethod_WithCombinedSpecifications_WorksAsExpected()
    {
        // Arrange
        var priceRangeRule = new PriceRangePaymentRule();
        var categoryRule = new ProductCategoryPaymentRule(_mockRepository.Object);
        
        // Create a specification that allows either:
        // 1. Food category products with Cash payment under 1000
        // 2. Electronics category products with Credit Card payment under 50000
        var combinedSpec = (categoryRule.And(priceRangeRule))
            .Or(new ProductCategoryPaymentRule(_mockRepository.Object).And(new PriceRangePaymentRule()));

        var service = new RuleBasedPaymentService(new IPaymentMethodRule[] { combinedSpec });

        // Test case 1: Food category with Cash payment under limit
        var foodProduct = new Product("Food Item", "Description", 500m, "Food") { Category = _foodCategory };
        var cashPayment = new PaymentMethod(PaymentMethodType.Cash, "Cash");

        // Test case 2: Electronics category with Credit Card payment under limit
        var electronicsProduct = new Product("Electronics Item", "Description", 5000m, "Electronics") { Category = _electronicsCategory };
        var creditCardPayment = new PaymentMethod(PaymentMethodType.CreditCard, "Credit Card");

        // Test case 3: Food category with Cash payment over limit
        var expensiveFoodProduct = new Product("Expensive Food", "Description", 1500m, "Food") { Category = _foodCategory };

        // Act & Assert
        Assert.True(await service.ValidatePaymentMethodAsync(foodProduct, cashPayment));
        Assert.True(await service.ValidatePaymentMethodAsync(electronicsProduct, creditCardPayment));
        Assert.False(await service.ValidatePaymentMethodAsync(expensiveFoodProduct, cashPayment));
    }
} 