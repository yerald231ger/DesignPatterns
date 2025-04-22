using PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Strategies;
using PaymentMethodDescriminator.Domain.Entities;

namespace PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Decorators;

public class CategoryValidatorDecorator : PaymentMethodValidatorDecorator
{
    private readonly ICategoryStrategy _categoryStrategy;

    public CategoryValidatorDecorator(
        IPaymentMethodValidator validator, 
        ICategoryStrategy categoryStrategy) 
        : base(validator)
    {
        _categoryStrategy = categoryStrategy;
    }

    public override bool IsValid(Product product, PaymentMethod paymentMethod)
    {
        if (!_categoryStrategy.IsValidForCategory(product.Category, paymentMethod.Type))
            return false;

        return base.IsValid(product, paymentMethod);
    }
} 