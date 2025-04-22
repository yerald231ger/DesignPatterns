using PaymentMethodDescriminator.Domain.Entities;

namespace PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Decorators;

public class BasePaymentMethodValidator : IPaymentMethodValidator
{
    public virtual bool IsValid(Product product, PaymentMethod paymentMethod)
    {
        return true; // Base case - always valid
    }
} 