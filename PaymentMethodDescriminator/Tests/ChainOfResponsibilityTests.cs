using PaymentMethodDescriminator.Approaches.ChainOfResponsibility.Handlers;
using PaymentMethodDescriminator.Approaches.ChainOfResponsibility.Services;
using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Enums;
using Xunit;

namespace PaymentMethodDescriminator.Tests;

public class ChainOfResponsibilityTests
{
    private readonly ChainPaymentService _service;

    public ChainOfResponsibilityTests()
    {
        _service = new ChainPaymentService();
    }

    [Fact]
    public void ValidatePaymentMethod_WhenAllHandlersPass_ReturnsTrue()
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
        var product = new Product("Test Product", "Description", 100m, "Food");
        var paymentMethod = new PaymentMethod(PaymentMethodType.CreditCard, "Credit Card");

        // Act
        var result = _service.ValidatePaymentMethod(product, paymentMethod);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(500, "Food", PaymentMethodType.Cash, true)]
    [InlineData(1500, "Food", PaymentMethodType.Cash, false)]
    [InlineData(500, "Food", PaymentMethodType.CreditCard, false)]
    [InlineData(500, "Electronics", PaymentMethodType.CreditCard, true)]
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
} 