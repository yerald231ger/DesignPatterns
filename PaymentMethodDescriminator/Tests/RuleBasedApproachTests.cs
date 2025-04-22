using PaymentMethodDescriminator.Approaches.RuleBasedApproach.Rules;
using PaymentMethodDescriminator.Approaches.RuleBasedApproach.Services;
using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Enums;
using PaymentMethodDescriminator.Domain.Repositories;
using Xunit;
using Moq;

namespace PaymentMethodDescriminator.Tests;

public class RuleBasedApproachTests
{
    private readonly Mock<ICategoryPaymentRuleRepository> _mockRepository;

    public RuleBasedApproachTests()
    {
        _mockRepository = new Mock<ICategoryPaymentRuleRepository>();
        _mockRepository.Setup(r => r.GetAllActiveRulesAsync())
            .ReturnsAsync(new List<CategoryPaymentRule>
            {
                new() { Id = 1, Category = "Food", PaymentMethodType = PaymentMethodType.Cash, IsActive = true },
                new() { Id = 2, Category = "Food", PaymentMethodType = PaymentMethodType.SodexoVoucher, IsActive = true },
                new() { Id = 3, Category = "Electronics", PaymentMethodType = PaymentMethodType.CreditCard, IsActive = true },
                new() { Id = 4, Category = "Electronics", PaymentMethodType = PaymentMethodType.DebitCard, IsActive = true }
            });
    }

    [Fact]
    public async Task ValidatePaymentMethod_WhenProductPriceIsWithinRange_ReturnsTrue()
    {
        // Arrange
        var rules = new IPaymentMethodRule[] { new PriceRangePaymentRule() };
        var service = new RuleBasedPaymentService(rules);
        var product = new Product("Test Product", "Description", 500m, "Electronics");
        var paymentMethod = new PaymentMethod(PaymentMethodType.Cash, "Cash");

        // Act
        var result = service.ValidatePaymentMethod(product, paymentMethod);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ValidatePaymentMethod_WhenProductPriceExceedsRange_ReturnsFalse()
    {
        // Arrange
        var rules = new IPaymentMethodRule[] { new PriceRangePaymentRule() };
        var service = new RuleBasedPaymentService(rules);
        var product = new Product("Test Product", "Description", 1500m, "Electronics");
        var paymentMethod = new PaymentMethod(PaymentMethodType.Cash, "Cash");

        // Act
        var result = service.ValidatePaymentMethod(product, paymentMethod);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidatePaymentMethod_WhenCategoryAllowsPaymentMethod_ReturnsTrue()
    {
        // Arrange
        var rules = new IPaymentMethodRule[] { new ProductCategoryPaymentRule(_mockRepository.Object) };
        var service = new RuleBasedPaymentService(rules);
        var product = new Product("Test Product", "Description", 100m, "Food");
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
        var product = new Product("Test Product", "Description", 100m, "Food");
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
        var product = new Product("Test Product", "Description", 500m, "Food");
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
        var product = new Product("Test Product", "Description", 1500m, "Food");
        var paymentMethod = new PaymentMethod(PaymentMethodType.Cash, "Cash");

        // Act
        var result = await service.ValidatePaymentMethodAsync(product, paymentMethod);

        // Assert
        Assert.False(result);
    }
} 