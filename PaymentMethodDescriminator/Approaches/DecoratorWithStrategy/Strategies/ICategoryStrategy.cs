using PaymentMethodDescriminator.Domain.Enums;

namespace PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Strategies;
 
public interface ICategoryStrategy
{
    bool IsValidForCategory(string category, PaymentMethodType paymentType);
} 