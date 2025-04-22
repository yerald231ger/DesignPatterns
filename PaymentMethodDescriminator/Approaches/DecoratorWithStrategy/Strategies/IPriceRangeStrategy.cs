using PaymentMethodDescriminator.Domain.Enums;

namespace PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Strategies;
 
public interface IPriceRangeStrategy
{
    bool IsInRange(decimal price, PaymentMethodType paymentType);
} 