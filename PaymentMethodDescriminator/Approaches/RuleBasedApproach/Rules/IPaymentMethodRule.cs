using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Enums;

namespace PaymentMethodDescriminator.Approaches.RuleBasedApproach.Rules;
 
public interface IPaymentMethodRule
{
    bool CanUsePaymentMethod(Product product, PaymentMethod paymentMethod);
} 