using PaymentMethodDescriminator.Domain.Entities;

namespace PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Decorators;

public abstract class PaymentMethodValidatorDecorator : IPaymentMethodValidator
{
    protected readonly IPaymentMethodValidator _validator;

    protected PaymentMethodValidatorDecorator(IPaymentMethodValidator validator)
    {
        _validator = validator;
    }

    public virtual bool IsValid(Product product, PaymentMethod paymentMethod)
    {
        return _validator.IsValid(product, paymentMethod);
    }
} 