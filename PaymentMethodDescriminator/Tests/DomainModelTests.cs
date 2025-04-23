using Moq;
using PaymentMethodDescriminator.Approaches.RuleBasedApproach.Rules;
using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Enums;
using PaymentMethodDescriminator.Domain.Repositories;
using Xunit;

namespace PaymentMethodDescriminator.Tests;

public class DomainModelTests
{
    private readonly Mock<ICategoryPaymentRuleRepository> _mockRepository;
    private readonly Category _foodCategory;
    private readonly Category _electronicsCategory;

    public DomainModelTests()
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
                }
            });
    }

    [Fact]
    public void Product_WithStandardRules_ValidatesPaymentMethodsCorrectly()
    {
        // Arrange
        var product = Product.CreateWithStandardRules(
            "Food Item",
            "Description",
            500m,
            _foodCategory,
            _mockRepository.Object
        );

        var validPayment = new PaymentMethod(PaymentMethodType.Cash, "Cash");
        var invalidPayment = new PaymentMethod(PaymentMethodType.CreditCard, "Credit Card");

        // Act & Assert
        Assert.True(product.AcceptsPaymentMethod(validPayment));
        Assert.False(product.AcceptsPaymentMethod(invalidPayment));
    }

    [Fact]
    public void Product_WithCustomRules_ValidatesPaymentMethodsCorrectly()
    {
        // Arrange
        var customRule = new PriceRangePaymentRule()
            .And(new ProductCategoryPaymentRule(_mockRepository.Object))
            .Or(new PriceRangePaymentRule()); // Fallback to price-only validation

        var product = Product.CreateWithCustomRules(
            "Premium Food Item",
            "Description",
            1500m,
            _foodCategory,
            customRule
        );

        var validPayment = new PaymentMethod(PaymentMethodType.CreditCard, "Credit Card");
        var invalidPayment = new PaymentMethod(PaymentMethodType.Cash, "Cash"); // Cash not allowed for high prices

        // Act & Assert
        Assert.True(product.AcceptsPaymentMethod(validPayment));
        Assert.False(product.AcceptsPaymentMethod(invalidPayment));
    }

    [Fact]
    public void Product_WithoutPaymentSpecification_ThrowsException()
    {
        // Arrange
        var product = new Product("Test Product", "Description", 100m, "Food");
        var payment = new PaymentMethod(PaymentMethodType.Cash, "Cash");

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(
            () => product.AcceptsPaymentMethod(payment)
        );
        Assert.Equal("Payment specifications not configured for this product.", exception.Message);
    }
} 