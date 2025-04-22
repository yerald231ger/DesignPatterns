using PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Decorators;
using PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Services;
using PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Strategies;
using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Enums;
using Xunit;

namespace PaymentMethodDescriminator.Tests;

public class DecoratorWithStrategyTests
{
    private readonly DecoratorPaymentService _service;

    public DecoratorWithStrategyTests()
    {
        _service = new DecoratorPaymentService();
    }

    [Fact]
    public void ValidatePaymentMethod_WhenAllValidatorsPass_ReturnsTrue()
    {
        // Arrange
        var product = new Product("Test Product", "Description", 500m, "Food");
        var paymentMethod = new PaymentMethod(PaymentMethodType.Cash, "Cash");

        // Act
        var result = _service.ValidatePaymentMethod(product, paymentMethod);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidatePaymentMethod_WhenPriceExceedsRange_ReturnsFalse()
    {
        // Arrange
        var product = new Product("Test Product", "Description", 1500m, "Food");
        var paymentMethod = new PaymentMethod(PaymentMethodType.Cash, "Cash");

        // Act
        var result = _service.ValidatePaymentMethod(product, paymentMethod);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidatePaymentMethod_WhenCategoryDoesNotAllowPaymentMethod_ReturnsFalse()
    {
        // Arrange
        var product = new Product("Test Product", "Description", 100m, "Electronics");
        var paymentMethod = new PaymentMethod(PaymentMethodType.SodexoVoucher, "Sodexo");

        // Act
        var result = _service.ValidatePaymentMethod(product, paymentMethod);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(500, "Food", PaymentMethodType.Cash, true)]
    [InlineData(1500, "Food", PaymentMethodType.Cash, false)]
    [InlineData(500, "Food", PaymentMethodType.SodexoVoucher, true)]
    [InlineData(500, "Electronics", PaymentMethodType.SodexoVoucher, false)]
    public void ValidatePaymentMethod_WithVariousScenarios(
        decimal price, 
        string category, 
        PaymentMethodType paymentType, 
        bool expectedResult)
    {
        // Arrange
        var product = new Product("Test Product", "Description", price, category);
        var paymentMethod = new PaymentMethod(paymentType, paymentType.ToString());

        // Act
        var result = _service.ValidatePaymentMethod(product, paymentMethod);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void ValidatePaymentMethod_WithCustomStrategy_UsesCustomValidation()
    {
        // Arrange
        var baseValidator = new BasePaymentMethodValidator();
        var customPriceStrategy = new CustomPriceRangeStrategy();
        var validator = new PriceRangeValidatorDecorator(baseValidator, customPriceStrategy);
        var product = new Product("Test Product", "Description", 2000m, "Food");
        var paymentMethod = new PaymentMethod(PaymentMethodType.Cash, "Cash");

        // Act
        var result = validator.IsValid(product, paymentMethod);

        // Assert
        Assert.True(result);
    }
}

// Custom strategy for testing
public class CustomPriceRangeStrategy : IPriceRangeStrategy
{
    public bool IsInRange(decimal price, PaymentMethodType paymentType)
    {
        return price <= 2000m; // Custom rule: all payments under 2000 are valid
    }
} 