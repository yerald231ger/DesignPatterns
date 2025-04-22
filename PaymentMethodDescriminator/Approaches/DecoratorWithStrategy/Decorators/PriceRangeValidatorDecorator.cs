using PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Strategies;
using PaymentMethodDescriminator.Domain.Entities;

namespace PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Decorators;

public class PriceRangeValidatorDecorator : PaymentMethodValidatorDecorator
{
    private readonly IPriceRangeStrategy _priceRangeStrategy;

    public PriceRangeValidatorDecorator(
        IPaymentMethodValidator validator, 
        IPriceRangeStrategy priceRangeStrategy) 
        : base(validator)
    {
        _priceRangeStrategy = priceRangeStrategy;
    }

    public override bool IsValid(Product product, PaymentMethod paymentMethod)
    {
        if (!_priceRangeStrategy.IsInRange(product.Price, paymentMethod.Type))
            return false;

        return base.IsValid(product, paymentMethod);
    }
} 