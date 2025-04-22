using PaymentMethodDescriminator.Domain.Entities;

namespace PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Decorators;
 
public interface IPaymentMethodValidator
{
    bool IsValid(Product product, PaymentMethod paymentMethod);
} 