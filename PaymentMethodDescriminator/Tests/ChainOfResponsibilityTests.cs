using PaymentMethodDescriminator.Approaches.ChainOfResponsibility.Handlers;
using PaymentMethodDescriminator.Approaches.ChainOfResponsibility.Services;
using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Enums;
using PaymentMethodDescriminator.Domain.Repositories;
using PaymentMethodDescriminator.Infrastructure.Repositories;
using PaymentMethodDescriminator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace PaymentMethodDescriminator.Tests;

public class ChainOfResponsibilityTests
{
    private readonly ChainPaymentService _service;
    private readonly ICategoryPaymentRuleRepository _repository;
    private readonly PaymentDbContext _dbContext;

    public ChainOfResponsibilityTests()
    {
        var options = new DbContextOptionsBuilder<PaymentDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        _dbContext = new PaymentDbContext(options);
        _dbContext.Database.OpenConnection();
        _dbContext.Database.EnsureCreated();

        // Seed initial data
        var foodCategory = new Category { Name = "Food" };
        var electronicsCategory = new Category { Name = "Electronics" };
        _dbContext.Categories.AddRange(foodCategory, electronicsCategory);

        var cashPayment = new PaymentMethod(PaymentMethodType.Cash, "Cash");
        var creditCardPayment = new PaymentMethod(PaymentMethodType.CreditCard, "Credit Card");
        var sodexoPayment = new PaymentMethod(PaymentMethodType.SodexoVoucher, "Sodexo");
        _dbContext.PaymentMethods.AddRange(cashPayment, creditCardPayment, sodexoPayment);

        _dbContext.CategoryPaymentMethodRules.AddRange(
            new CategoryPaymentMethodRule { Category = foodCategory, PaymentMethod = cashPayment, IsActive = true },
            new CategoryPaymentMethodRule { Category = foodCategory, PaymentMethod = sodexoPayment, IsActive = true },
            new CategoryPaymentMethodRule { Category = electronicsCategory, PaymentMethod = creditCardPayment, IsActive = true }
        );

        _dbContext.SaveChanges();

        _repository = new CategoryPaymentRuleRepository(_dbContext);
        _service = new ChainPaymentService(_repository);
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