using PaymentMethodDescriminator.Approaches.RuleBasedApproach.Rules;
using PaymentMethodDescriminator.Approaches.RuleBasedApproach.Services;
using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Enums;
using Xunit;

namespace PaymentMethodDescriminator.Tests;

public class RuleBasedApproachTests
{
    [Fact]
    public void ValidatePaymentMethod_WhenProductPriceIsWithinRange_ReturnsTrue()
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
    public void ValidatePaymentMethod_WhenProductPriceExceedsRange_ReturnsFalse()
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
    public void ValidatePaymentMethod_WhenCategoryAllowsPaymentMethod_ReturnsTrue()
    {
        // Arrange
        var rules = new IPaymentMethodRule[] { new ProductCategoryPaymentRule() };
        var service = new RuleBasedPaymentService(rules);
        var product = new Product("Test Product", "Description", 100m, "Food");
        var paymentMethod = new PaymentMethod(PaymentMethodType.Cash, "Cash");

        // Act
        var result = service.ValidatePaymentMethod(product, paymentMethod);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidatePaymentMethod_WhenCategoryDoesNotAllowPaymentMethod_ReturnsFalse()
    {
        // Arrange
        var rules = new IPaymentMethodRule[] { new ProductCategoryPaymentRule() };
        var service = new RuleBasedPaymentService(rules);
        var product = new Product("Test Product", "Description", 100m, "Food");
        var paymentMethod = new PaymentMethod(PaymentMethodType.DebitCard, "Debit Card");

        // Act
        var result = service.ValidatePaymentMethod(product, paymentMethod);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidatePaymentMethod_WhenAllRulesPass_ReturnsTrue()
    {
        // Arrange
        var rules = new IPaymentMethodRule[] 
        { 
            new PriceRangePaymentRule(),
            new ProductCategoryPaymentRule()
        };
        var service = new RuleBasedPaymentService(rules);
        var product = new Product("Test Product", "Description", 500m, "Food");
        var paymentMethod = new PaymentMethod(PaymentMethodType.Cash, "Cash");

        // Act
        var result = service.ValidatePaymentMethod(product, paymentMethod);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidatePaymentMethod_WhenAnyRuleFails_ReturnsFalse()
    {
        // Arrange
        var rules = new IPaymentMethodRule[] 
        { 
            new PriceRangePaymentRule(),
            new ProductCategoryPaymentRule()
        };
        var service = new RuleBasedPaymentService(rules);
        var product = new Product("Test Product", "Description", 1500m, "Food");
        var paymentMethod = new PaymentMethod(PaymentMethodType.Cash, "Cash");

        // Act
        var result = service.ValidatePaymentMethod(product, paymentMethod);

        // Assert
        Assert.False(result);
    }
} 