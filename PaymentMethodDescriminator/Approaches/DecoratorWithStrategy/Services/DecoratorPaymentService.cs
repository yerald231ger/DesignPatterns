using PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Decorators;
using PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Strategies;
using PaymentMethodDescriminator.Domain.Entities;

namespace PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Services;

public class DecoratorPaymentService
{
    private readonly IPaymentMethodValidator _validator;

    public DecoratorPaymentService()
    {
        // Build the validator with decorators
        IPaymentMethodValidator validator = new BasePaymentMethodValidator();
        validator = new PriceRangeValidatorDecorator(validator, new DefaultPriceRangeStrategy());
        validator = new CategoryValidatorDecorator(validator, new DefaultCategoryStrategy());
        
        _validator = validator;
    }

    public bool ValidatePaymentMethod(Product product, PaymentMethod paymentMethod)
    {
        return _validator.IsValid(product, paymentMethod);
    }
} 